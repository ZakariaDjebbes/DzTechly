import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/modules/account/account.service';
import { CartService } from 'src/app/modules/cart/cart.service';
import { ICart } from 'src/app/shared/models/Cart';
import { IUser } from 'src/app/shared/models/IUser';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    isCollapsed: boolean;
    cart$: Observable<ICart>;
    user$: Observable<IUser>;

    constructor(private cartService: CartService, private accountService: AccountService) { }

    ngOnInit(): void {
        this.isCollapsed = true;
        this.cart$ = this.cartService.cart$;
        this.user$ = this.accountService.currentUser$;
    }

    logout(): void {
        this.accountService.logout();
    }
}
