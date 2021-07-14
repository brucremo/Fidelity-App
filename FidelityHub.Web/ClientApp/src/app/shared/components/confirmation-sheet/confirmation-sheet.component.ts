import { Component, ComponentFactoryResolver, ContentChild, Directive, Inject, OnInit, TemplateRef, ViewChild, ViewContainerRef } from "@angular/core";
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from "@angular/material/bottom-sheet";

@Component({
  selector: "app-confirmation-sheet",
  templateUrl: "./confirmation-sheet.component.html",
  styleUrls: ["./confirmation-sheet.component.css"],
})
export class ConfirmationSheetComponent implements OnInit {
  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private sheetRef: MatBottomSheetRef<ConfirmationSheetComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: ConfirmationData
  ) {}

  ngOnInit(): void {
  }

  public closeSheet(): void {
    this.sheetRef.dismiss();
  }

  get title(): string {
    return this.data.title;
  }

  get component(): any {
    return this.data.component;
  }
}

export class ConfirmationData {
  constructor(public title: string, public component: any){}
} 