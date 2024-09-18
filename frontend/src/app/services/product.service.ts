import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appSettings } from '../settings/appSettings';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private http = inject(HttpClient);
  private apiUrl: string = appSettings.apiUrl + 'product';

  constructor() { }

  getAll() {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getOne(id: number) {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(product: Product) {
    return this.http.post<Product>(this.apiUrl, product);
  }

  update(product: Product) {
    return this.http.put<Product>(`${this.apiUrl}/${product.id}`, product);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
