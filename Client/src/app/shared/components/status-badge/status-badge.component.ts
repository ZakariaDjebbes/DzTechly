import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-status-badge',
  templateUrl: './status-badge.component.html',
  styleUrls: ['./status-badge.component.scss']
})
export class StatusBadgeComponent implements OnInit {
  @Input() content: any;

  constructor() { }

  ngOnInit(): void {
  }
}