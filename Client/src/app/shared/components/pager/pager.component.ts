import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();

  page = 1;

  constructor() { }

  ngOnInit(): void {
  }

  // tslint:disable-next-line: typedef
  public OnPageChanged(page: number)
  {
    this.pageChanged.emit(page);
  }
}
