import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { LayoutModule } from '@angular/cdk/layout';
import { FlexLayoutModule } from '@angular/flex-layout'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { ContactComponent } from './contact/contact.component';
import { TermsConditionsComponent } from './terms-conditions/terms-conditions.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { MatExpansionModule } from '@angular/material/expansion'; 
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    data: { animation : "Home"}
  },
  {
    path: '',
    component: HomeComponent,
    data: { animation : "Home"}
  },
  {
    path: 'contato',
    component: ContactComponent,
    data: { animation : "Contact"}
  },
  {
    path: 'privacidade',
    component: TermsConditionsComponent,
    data: { animation : "Contact"}
  }
];

@NgModule({
  declarations: [HomeComponent, ContactComponent, TermsConditionsComponent, AboutUsComponent],
  imports: [
    CommonModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    LayoutModule,
    FlexLayoutModule,
    MatFormFieldModule,
    MatExpansionModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class WebpageModule { }
