import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavTitleService {

  private title: string;

  constructor() { }

  get navTitle(): string {
    return this.title;
  }

  public setNavTitle(title: string): void {
    this.title = title;
  }

  public clearNavTitle(): void {
    this.title = null;
  }
}
