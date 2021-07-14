import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private isBusy: boolean;

  constructor() {
  }

  public startLoading(): void {
    this.isBusy = true;
  }

  public loadingComplete(): void {
    this.isBusy = false;
  }

  get loadingStatus(): boolean {
    return this.isBusy;
  }
}
