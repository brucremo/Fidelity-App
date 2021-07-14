import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  public showMessage(message: string, action: string = "OK"): void {
    this.snackBar.open(message, action, {
      duration: 5000
    });
  }
}
