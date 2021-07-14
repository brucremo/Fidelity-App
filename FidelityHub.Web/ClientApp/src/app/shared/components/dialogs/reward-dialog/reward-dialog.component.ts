import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RewardDialogData } from "../../../models/dialogs/reward-dialog";

@Component({
  selector: "app-reward-dialog",
  templateUrl: "./reward-dialog.component.html",
  styleUrls: ["./reward-dialog.component.css"],
})
export class RewardDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<RewardDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: RewardDialogData
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
