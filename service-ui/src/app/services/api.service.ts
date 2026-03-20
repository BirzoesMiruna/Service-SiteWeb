import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface ServiciuDto {
  id: string;          // la tine e GUID
  nume: string;
  descriere: string;
  imagineUrl?: string; // <-- important
}

export interface RecenzieDto {
  id: string;           // sau number – pune ce ai tu în API-GUID
  serviciuId: string;  
  text: string;
  rating: number;       // 1-5
  createdAt?: string;        // ISO date (opțional)
}




@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiBase;

  getServicii() {
    return this.http.get<ServiciuDto[]>(`${this.base}/api/Servicii`);
  }
  getRecenzii() {
  return this.http.get<RecenzieDto[]>(`${this.base}/api/Recenzii`);
}
postRecenzie(dto: { serviciuId: string; text: string; rating: number; }) {
  return this.http.post(`${this.base}/api/Recenzii`, dto);
}

}
