import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-info-modal',
  templateUrl: './add-info-modal.component.html',
  styleUrls: ['./add-info-modal.component.scss']
})
export class AddInfoModalComponent implements OnInit {
  @ViewChild('info') info: ElementRef;
  @ViewChild('unit') unit: ElementRef;

  res: string[] = ['', ''];
  errors: any[] = null;

  constructor(public activeModal: NgbActiveModal) {
  }

  ngOnInit(): void {
  }

  OnInfoChange(): void {
    this.res[0] = this.info.nativeElement.value;
  }

  OnUnitChange(): void {
    this.res[1] = this.unit.nativeElement.value;
  }

  closeModal(): void {
    if(this.res[0] !== null && this.res[0] !== '')
      this.activeModal.close(this.res);
    else
      this.errors = ["Information name is required"];
  }
}
