import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  loadingCount = 0;
  constructor(private spinner: NgxSpinnerService) { }

  busy(): void {
    this.loadingCount++;
    this.spinner.show(undefined, {
      type: 'ball-pulse',
      bdColor: 'rgba(0, 0, 0, 0.8)',
      color: '#FFFAFA',
      size: 'large'
    });
  }

  idle(): void {
    this.loadingCount--;

    if (this.loadingCount <= 0) {
      this.loadingCount = 0;
      this.spinner.hide();
    }
  }
}
