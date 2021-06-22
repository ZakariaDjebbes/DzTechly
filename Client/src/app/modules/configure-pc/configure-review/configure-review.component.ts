import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfigurePcComponent } from '../configure-pc.component';

@Component({
  selector: 'app-configure-review',
  templateUrl: './configure-review.component.html',
  styleUrls: ['./configure-review.component.scss']
})
export class ConfigureReviewComponent implements OnInit {
  @Input() parent: ConfigurePcComponent;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }
}
