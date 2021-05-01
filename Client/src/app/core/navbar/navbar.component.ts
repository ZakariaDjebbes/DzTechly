import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CartService } from 'src/app/modules/cart/cart.service';
import { ICart } from 'src/app/shared/models/Cart';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    isCollapsed: boolean;
    cart$: Observable<ICart>;

    constructor(private cartService: CartService ) { }

    ngOnInit(): void {
        this.isCollapsed = true;
        this.cart$ = this.cartService.cart$;
    }
}
