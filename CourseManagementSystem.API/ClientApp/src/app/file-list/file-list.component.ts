import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {RoleAuthService} from '../role-auth.service';
import {FileService} from '../file.service';
import {FileVM} from '../viewmodels/fileVM';
import {CourseService} from '../course.service';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {

  @ViewChild('inputFile', null)
  private inputFile: ElementRef;

  @Input()
  private courseId: string;

  private fileService: FileService;

  public isAdmin: boolean;
  public fileToUpload: File;
  public uploadedFiles: FileVM[] = [];

  constructor(roleAuthService: RoleAuthService, fileService: FileService, courseService: CourseService) {
    this.fileService = fileService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });

    courseService.getAllFiles(this.courseId).subscribe(result => {
      this.uploadedFiles = result;
    });
  }

  ngOnInit() {
  }

  // On file Select
  onChange(event) {
    this.fileToUpload = event.target.files[0];
  }

  public uploadFile(): void {
    this.fileService.uploadTo(this.fileToUpload, this.courseId).subscribe(result => {
      this.uploadedFiles.push(result);
    });
    this.fileToUpload = null;
    this.inputFile.nativeElement.value = ''; // clear file input
  }

  public removeFile(fileId: number): void {
    this.fileService.delete(fileId).subscribe();
    this.uploadedFiles = this.uploadedFiles.filter(f => f.id !== fileId);
  }

  public downloadFile(fileId: number): void {
    this.fileService.download(fileId);
  }
}
