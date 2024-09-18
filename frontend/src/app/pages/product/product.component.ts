import { Component, Input, OnInit, inject } from '@angular/core';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/Product';

interface Status {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [MatFormFieldModule, MatSelectModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {
  @Input('id') productId!: number;
  private productService = inject(ProductService);
  public formBuild = inject(FormBuilder);
  selected = ''

  public productForm: FormGroup = this.formBuild.group({
    name: [''],
    price: [0],
    quantity: [0],
    productStatus: ['Active'],
  });

  status: Status[] = [
    { value: 'Active', viewValue: 'Activo' },
    { value: 'OutOfStock', viewValue: 'Agotado' },
    { value: 'Inactive', viewValue: 'Inactivo' }
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {
    if (this.productId != 0) {
      this.productService.getOne(this.productId).subscribe({
        next: (data) => {
          this.selected = data.productStatus;
          this.productForm.patchValue({
            name: data.name,
            price: data.price,
            quantity: data.quantity,
            productStatus: data.productStatus,
          })
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }

  save() {
    const product: Product = {
      id: this.productId,
      name: this.productForm.value.name,
      price: parseFloat(this.productForm.value.price),
      quantity: this.productForm.value.quantity,
      productStatus: this.productForm.value.productStatus
    }

    if (this.productId == 0) {
      const { id, ...rest } = product;
      this.productService.create(rest).subscribe({
        next: (data) => {
          if (data?.name) {
            this.router.navigate(["/"]);
          } else {
            alert("Error al crear")
            alert("Ooops! Algunos datos no son validos")

          }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    } else {
      this.productService.update(product).subscribe({
        next: (data) => {
          if (data?.name) {
            this.router.navigate(["/"]);
          } else {
            alert("Error al editar")
          }
        },
        error: (err) => {
          console.log(err.message)
          alert("Ooops! Algunos datos no son validos")

        }
      })
    }


  }

  goBack() {
    this.router.navigate(["/"]);
  }
}
