import { Component, OnInit, Input, ViewChild } from "@angular/core";
import { MatMenu } from "@angular/material/menu";
import { Router } from "@angular/router";
import { NavItem } from "../../interfaces/nav-item.interface";

@Component({
  selector: "app-menu-item",
  templateUrl: "./menu-item.component.html",
  styleUrls: ["./menu-item.component.css"],
})
export class MenuItemComponent implements OnInit {

  @Input() items: NavItem[];
  @ViewChild("childMenu") public childMenu;

  constructor(public router: Router) {
  }

  ngOnInit(): void {}
}
