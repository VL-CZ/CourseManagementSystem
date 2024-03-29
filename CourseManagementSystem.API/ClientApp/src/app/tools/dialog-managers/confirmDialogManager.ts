import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmButtonStyle, ConfirmDialogComponent} from '../../components/confirm-dialog/confirm-dialog.component';

export class ConfirmDialogManager {
  public static readonly irreversibleActionMessage = 'This action cannot be reverted!';

  public constructor(private bsModalRef: BsModalRef, private modalService: BsModalService) {
  }

  /**
   * display a confirmation dialog
   * @param title title of the dialog
   * @param text text of the dialog (will be appended by {@link irreversibleActionMessage}
   * @param confirmButtonStyle style of the confirmtaion button in the dialog
   * @param confirmAction action to do on dialog confirmation
   */
  public displayDialog(title: string, text: string, confirmAction: () => void,
                       confirmButtonStyle: ConfirmButtonStyle = ConfirmButtonStyle.Warning) {
    const initialState = {
      title: title,
      text: text,
      textAppendix: ConfirmDialogManager.irreversibleActionMessage,
      confirmButtonStyle: confirmButtonStyle,
      onConfirm: confirmAction
    };

    this.bsModalRef = this.modalService.show(ConfirmDialogComponent, {initialState});
  }
}
