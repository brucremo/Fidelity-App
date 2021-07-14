import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserType } from 'src/app/shared/models/userRegistration';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy{

  constructor(
    private router: Router
    ) {
  }

  public userTypes: any[] = [
    { id: UserType.Customer, name: "Cliente", description: "Entre para nossa plataforma e nunca mais perca um cartão de fidelidade!", icon: "people", disabled: false },
    { id: UserType.Vendor, name: "Lojista", description: "Em breve...", icon: "store", disabled: true },
  ];

  private selectedUserType: string = null;

  ngOnInit() {
    if(history.state.userType){
      this.setUserType(this.userTypes.find(x => x.id == history.state.userType));
    }
  }

  onSubmit() {
  }

  ngOnDestroy(){
    this.selectedUserType = null;
  }

  // --- UI Event Handling ---
  public setUserType(userType: any): void {
    if(userType.disabled){
      return;
    }
    this.selectedUserType = userType.name;
  }

  // --- Getters ---
  get isVendorSelected(): boolean {
    return this.selectedUserType == "Lojista";
  }

  get isClientSelected(): boolean {
    return this.selectedUserType == "Cliente";
  }

  get isNoneSelected(): boolean {
    return this.selectedUserType == null || this.selectedUserType == undefined;
  }
}
