
export type ProductStatus = 'Active' | 'OutOfStock' | 'Inactive'

export interface Product {
  id?: number,
  name: string,
  price: number,
  quantity: number,
  productStatus: ProductStatus,
  creationDate?: Date,
  updatedDate?: Date
}
