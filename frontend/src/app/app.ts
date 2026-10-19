import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { DeviceService } from './services/device.service';
import { CreateDeviceRequest, Device, HealthResponse } from './models/device.model';

@Component({
  selector: 'app-root',
  imports: [CommonModule, FormsModule, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private readonly deviceService = inject(DeviceService);

  protected readonly title = signal('ReDevice');
  protected readonly health = signal<HealthResponse | null>(null);
  protected readonly devices = signal<Device[]>([]);
  protected readonly loading = signal(false);
  protected readonly errorMessage = signal<string | null>(null);

  protected newDevice: CreateDeviceRequest = {
    name: '',
    serialNumber: '',
    category: 'Smartphone',
    condition: 'Refurbished - Grade A',
    price: 0,
    isAvailable: true
  };

  ngOnInit(): void {
    this.checkHealth();
    this.loadDevices();
  }

  checkHealth(): void {
    this.deviceService.getHealth().subscribe({
      next: (res) => this.health.set(res),
      error: () => this.health.set(null)
    });
  }

  loadDevices(): void {
    this.loading.set(true);
    this.errorMessage.set(null);

    this.deviceService.getAll().subscribe({
      next: (items) => {
        this.devices.set(items);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.errorMessage.set(
          'Could not load devices. Make sure the ASP.NET Core API and PostgreSQL database are running.'
        );
      }
    });
  }

  createDevice(): void {
    if (!this.newDevice.name || !this.newDevice.serialNumber) {
      return;
    }

    this.errorMessage.set(null);
    this.deviceService.create(this.newDevice).subscribe({
      next: (created) => {
        this.devices.update((list) => [created, ...list]);
        this.newDevice = {
          name: '',
          serialNumber: '',
          category: 'Smartphone',
          condition: 'Refurbished - Grade A',
          price: 0,
          isAvailable: true
        };
      },
      error: (err) => {
        this.errorMessage.set(err?.error?.detail || 'Failed to create device.');
      }
    });
  }

  deleteDevice(id: string): void {
    this.deviceService.delete(id).subscribe({
      next: () => {
        this.devices.update((list) => list.filter((d) => d.id !== id));
      },
      error: () => {
        this.errorMessage.set('Failed to delete device.');
      }
    });
  }
}
