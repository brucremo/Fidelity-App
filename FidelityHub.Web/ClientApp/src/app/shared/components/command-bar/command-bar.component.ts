import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

export interface INavigatedComponent {
  route: string;
  title: string;
}

export interface ISortOption {
  title: string;
  icon: string;
  subOptions?: ISortOption[];
}

export interface IFilterOption {
  title: string;
  icon: string;
  subOptions?: IFilterOption[];
}

@Component({
  selector: 'app-command-bar',
  templateUrl: './command-bar.component.html',
  styleUrls: ['./command-bar.component.css']
})
export class CommandBarComponent implements OnInit {

  // Routing support
  @Input() previousComponent: INavigatedComponent = null;
  @Input() nextComponent: INavigatedComponent = null;

  @Input() sortOptions: ISortOption[];
  @Input() filterOptions: IFilterOption[];

  constructor(
    public router: Router
  ) { }

  ngOnInit(): void {
  }

  public return(): void {
    this.router.navigate([this.previousComponent.route]);
  }

}
