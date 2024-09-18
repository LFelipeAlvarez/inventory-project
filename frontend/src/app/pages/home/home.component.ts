import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/Product';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { statusNormalized } from '../../consts';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatIconModule, MatButtonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private productService = inject(ProductService);
  public productsList: Product[] = [];
  public displayedColumns: string[] = ['id', 'name', 'price', 'quantity', 'productStatus', 'action'];
  public statusNormalized = {
    Active: 'Activo',
    Inactive: 'Inactivo',
    OutOfStock: 'Agotado'
  };

  constructor(private router: Router) {
    this.getProducts();
  }

  translateStatus(status: string): string {
    return this.statusNormalized[status as keyof typeof statusNormalized] || status;
  }

  getProducts() {
    this.productService.getAll().subscribe({
      next: (data) => {
        if (data?.length >= 0) {
          this.productsList = data;
        }
      },
      error: (err) => console.log(err)
    });
  }

  addProduct() {
    this.router.navigate(['/product', 0]);
  }

  updateProduct(product: Product) {
    this.router.navigate(['/product', product.id]);
  }

  deleteProduct(product: Product) {
    if (confirm(`Desea eliminar el producto: ${product.name}`)) {
      this.productService.delete(product.id!).subscribe({
        next: (data) => {
          this.getProducts()
        },
        error: (err) => console.log(err)
      });
    }
  }

}
