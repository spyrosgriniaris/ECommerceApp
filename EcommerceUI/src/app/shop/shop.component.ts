import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IProduct } from '../share/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../share/models/brand';
import { IProductType } from '../share/models/productType';
import { ShopParams } from '../share/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  productTypes: IProductType[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ]

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      response => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }, error => {
        console.log(error);
      }
    );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      response => {
        // this line adds another object in the array of the response on top of the array
        // this will add All in the array, to reset the filters a user may have chosen
        this.brands = [{id: 0, name: 'All'}, ...response];
      }, error => {
        console.log(error);
      }
    );
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(
      response => {
        this.productTypes = [{id: 0, name: 'All'}, ...response];
      }, error => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onProductTypeSelected(productTypeId: number) {
    this.shopParams.productTypeId = productTypeId;
    this.shopParams.pageNumber = 1
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    // if not equal, this means that user just changed page
    // otherwise, it means that page is changed because user may selected another type or brand to be displayed
    // and we git from page ex. 3 of a category to 1 of the new category selected
    // we don't want pagination to be called when we change category or type manually, so we call this method only when we change page manually
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

  // end of component
}
