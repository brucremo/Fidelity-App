import { Component, Input, OnInit, Output, EventEmitter, OnChanges, SimpleChanges } from "@angular/core";
import { MatBottomSheet } from "@angular/material/bottom-sheet";
import { Observable } from "rxjs";
import { ConfirmationData, ConfirmationSheetComponent } from "src/app/shared/components/confirmation-sheet/confirmation-sheet.component";
import { PromotionSale } from "src/app/shared/models/promotion";
import { Sale, SalesViewModel } from "src/app/shared/models/sales-model";
import { VendorUnit } from "../../models/vendor";
import { ApproveConfirmationComponent } from "../approve-confirmation/approve-confirmation.component";
import { DeleteConfirmationComponent } from "../delete-confirmation/delete-confirmation.component";
import { EditConfirmationComponent } from "../edit-confirmation/edit-confirmation.component";

@Component({
  selector: "app-sale-list",
  templateUrl: "./sale-list.component.html",
  styleUrls: ["./sale-list.component.css"],
})
export class SaleListComponent implements OnInit, OnChanges {
  
  @Input() viewModel: SalesViewModel;
  @Input() showCommands: boolean = true;

  @Output() verifyClicked: EventEmitter<PromotionSale> = new EventEmitter<PromotionSale>();
  @Output() editClicked: EventEmitter<PromotionSale> = new EventEmitter<PromotionSale>();
  @Output() removeClicked: EventEmitter<PromotionSale> = new EventEmitter<PromotionSale>();

  constructor(
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
  }

  ngOnInit(): void {
  }
  
  // Commands
  public sort(fn: any) : void {
    this.viewModel.sales.sort(fn);
  }

  get saleListModels(): Observable<PromotionSale[]> {
    return new Observable<PromotionSale[]>((observer) => {
      observer.next(this.viewModel.sales);
      observer.complete();
    })
  }

  get vendorUnit(): VendorUnit {
    return this.viewModel.vendorUnit;
  }

  // Helpers
  public getIconTooltip(sale: PromotionSale): string {
    return sale.approved ? "Aprovada" : "Aprovação Pendente"
  }

  public getSaleIcon(sale: PromotionSale): string {
    if(sale.removed){
      return "delete";
    }else{
      return sale.approved ? "check_circle" : "pending_actions";
    }
  }

  public isSaleApproved(sale: PromotionSale): boolean {
    return sale.approved;
  }

  public getVerifyButtonIcon(sale: PromotionSale): string {
    return sale.approved ? "unpublished" : "verified";
  }

  public getVerifyButtonTooltip(sale: PromotionSale): string {
    return sale.approved ? "Revogar Aprovação" : "Aprovar Venda";
  }

  public getRemoveButtonIcon(sale: PromotionSale): string {
    return sale.removed ? "restore_from_trash" : "delete_forever";
  }

  public getRemoveButtonTooltip(sale: PromotionSale): string {
    return sale.removed ? "Restaurar Venda" : "Remover venda";
  }

  // UI Event Handling
  public verifySale(sale: PromotionSale): void {
    this.verifyClicked.emit(sale);
  }

  public editSale(sale: PromotionSale): void {
    this.editClicked.emit(sale);
  }

  public removeSale(sale: PromotionSale): void {
    this.removeClicked.emit(sale);
  }
}
