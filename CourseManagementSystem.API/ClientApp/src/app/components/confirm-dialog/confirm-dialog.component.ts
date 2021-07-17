import {Component, OnInit} from '@angular/core';
import {BsModalRef} from 'ngx-bootstrap/modal';

/**
 * this component represents confirmation dialongs
 */
@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit {

  /**
   * Title of the dialog
   */
  public title: string;

  /**
   * Text of the dialog
   */
  public text: string;

  /**
   * appendix to the text (printed on newline)
   */
  public textAppendix: string;

  /**
   * style of the confirm button
   */
  public confirmButtonStyle: ConfirmButtonStyle;

  /**
   * action to execute on dialog confirmation
   */
  public onConfirm: () => void;

  public modalRef: BsModalRef;

  constructor(modalRef: BsModalRef) {
    this.modalRef = modalRef;
  }

  ngOnInit() {
  }

  /**
   * simple getter for accessing the enum in the template
   * @constructor
   */
  public get ConfirmButtonStyle() {
    return ConfirmButtonStyle;
  }

  /**
   * confirm the dialog
   */
  public confirm(): void {
    this.modalRef.hide();
    this.onConfirm();
  }
}

/**
 * enum that specifies the design of the 'confirm' button in the dialog
 */
export enum ConfirmButtonStyle {
  /**
   * the button is styled red (using btn-danger bootstrap class)
   */
  Warning,

  /**
   * the button is styled blue (using btn-info bootstrap class)
   */
  Information
}
