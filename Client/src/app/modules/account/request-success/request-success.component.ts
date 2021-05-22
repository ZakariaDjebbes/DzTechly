import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-request-success',
  templateUrl: './request-success.component.html',
  styleUrls: ['./request-success.component.scss']
})
export class RequestSuccessComponent implements OnInit {
  email: string;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation && navigation.extras && navigation.extras.state;
    if (state) {
      this.email = state as unknown as string;
    } else {
      this.router.navigateByUrl('/');
    }
   }

   ngOnInit(): void
   {

   }
}
