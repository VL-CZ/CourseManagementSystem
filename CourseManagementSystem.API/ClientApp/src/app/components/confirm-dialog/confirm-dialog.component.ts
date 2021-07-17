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
   * confirm the dialog
   */
  public confirm(): void {
    this.modalRef.hide();
    this.onConfirm();
  }
}
