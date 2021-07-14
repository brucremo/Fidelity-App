import { Component, OnInit } from '@angular/core';
import { NavTitleService } from 'src/app/shared/services/nav-title.service';

@Component({
  selector: 'app-terms-conditions',
  templateUrl: './terms-conditions.component.html',
  styleUrls: ['./terms-conditions.component.css']
})
export class TermsConditionsComponent implements OnInit {

  private title: string = "Termos e Privacidade";

  constructor(
    public titleService: NavTitleService
  ) {
    this.titleService.setNavTitle(this.title);
  }

  ngOnInit(): void {
  }

}
