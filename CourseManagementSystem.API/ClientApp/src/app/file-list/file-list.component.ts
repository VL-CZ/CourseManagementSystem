import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RoleAuthService} from '../role-auth.service';
import {FileService} from '../file.service';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {

  @ViewChild('inputFile', null)
  private inputFile: ElementRef;

  private fileService: FileService;

  public isAdmin: boolean;
  public fileToUpload: File;
  public fileIDs: number[] = [];

  constructor(roleAuthService: RoleAuthService, fileService: FileService) {
    this.fileService = fileService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }

  // On file Select
  onChange(event) {
    this.fileToUpload = event.target.files[0];
  }

  public uploadFile(): void {
    this.fileService.uploadFile(this.fileToUpload).subscribe(result => {
      this.fileIDs.push(result);
    });
    this.fileToUpload = null;
    this.inputFile.nativeElement.value = '';
  }
}
