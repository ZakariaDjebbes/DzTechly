import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/modules/account/account.service';
import { IUser } from 'src/app/shared/models/IUser';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;
  user$: Observable<IUser>;

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>,
              private accountService: AccountService) { }

  ngOnInit(): void { 
    this.user$ = this.accountService.currentUser$;

    this.user$.subscribe(
      user => {
        if (this.accountService.checkRoles(this.appHasRole, user)) {
          if (!this.isVisible) {
            this.isVisible = true;
            this.viewContainerRef.createEmbeddedView(this.templateRef);
          }
        }
        else {
          this.viewContainerRef.clear();
          this.isVisible = false;
        }
      },
      error => {
        console.error(error);
      }
    );
  }
}
