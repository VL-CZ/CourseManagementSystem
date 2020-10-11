import {Component, OnInit} from '@angular/core';
import {RoleAuthService} from '../role-auth.service';
import {FileService} from '../file.service';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {

  private fileService: FileService;

  public isAdmin: boolean;
  public fileToUpload: File;

  constructor(roleAuthService: RoleAuthService, fileService: FileService) {
    this.fileService = fileService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }

  public uploadFile(): void {
    this.fileService.uploadFile(this.fileToUpload).subscribe();
  }
}
