export interface Device {
  id: string;
  name: string;
  serialNumber: string;
  category: string;
  condition: string;
  price: number;
  isAvailable: boolean;
  createdAtUtc: string;
  updatedAtUtc?: string | null;
}

export interface CreateDeviceRequest {
  name: string;
  serialNumber: string;
  category: string;
  condition: string;
  price: number;
  isAvailable: boolean;
}

export interface UpdateDeviceRequest {
  name: string;
  category: string;
  condition: string;
  price: number;
  isAvailable: boolean;
}

export interface HealthResponse {
  status: string;
  service: string;
  timestampUtc: string;
  layers: string[];
}

