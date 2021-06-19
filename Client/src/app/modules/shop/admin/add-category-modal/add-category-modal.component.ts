import { Component, ElementRef, OnInit, Output, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-category-modal',
  templateUrl: './add-category-modal.component.html',
  styleUrls: ['./add-category-modal.component.scss']
})
export class AddCategoryModalComponent implements OnInit {
  @ViewChild('category') category: ElementRef;
  @ViewChild('info') info: ElementRef;
  @ViewChild('unit') unit: ElementRef;

  res: string[] = ['', '', ''];
  errors: any[] = null;

  constructor(public activeModal: NgbActiveModal) {
  }

  ngOnInit(): void {
  }

  onCategoryChange(): void {
    this.res[0] = this.category.nativeElement.value;
  }

  onInfoChange(): void {
    this.res[1] = this.info.nativeElement.value;
  }

  onUnitChange(): void {
    this.res[2] = this.unit.nativeElement.value;
  }

  closeModal(): void {
    if(this.res[0] !== null && this.res[0] !== '' && this.res[1] !== null && this.res[1] !== '')
      this.activeModal.close(this.res);
    else
      this.errors = ["The category name and the first information name are required"];
  }
}
