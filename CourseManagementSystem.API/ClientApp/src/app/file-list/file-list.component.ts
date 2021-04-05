import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {RoleAuthService} from '../role-auth.service';
import {FileService} from '../file.service';
import {FileVM} from '../viewmodels/fileVM';
import {CourseService} from '../course.service';

/**
 * component representing list of files
 */
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
  private courseService: CourseService;

  /**
   * is the current user admin of the course?
   */
  public isAdmin: boolean;

  /**
   * file that will be uploaded
   */
  public fileToUpload: File;

  /**
   * list of uploaded files in this course
   */
  public uploadedFiles: FileVM[] = [];

  constructor(roleAuthService: RoleAuthService, fileService: FileService, courseService: CourseService) {
    this.fileService = fileService;
    this.courseService = courseService;

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
    this.reloadFileData();
  }

  /**
   * handler for file upload action
   * @param event
   */
  public onFileSelect(event): void {
    this.fileToUpload = event.target.files[0];
  }

  /**
   * upload a new file
   */
  public uploadFile(): void {
    this.fileService.uploadTo(this.fileToUpload, this.courseId).subscribe(() => {
      this.reloadFileData();
    });
    this.fileToUpload = null;
    this.inputFile.nativeElement.value = ''; // clear file input
  }

  /**
   * remove a file with given id
   * @param fileId identifier of the file
   */
  public removeFile(fileId: number): void {
    this.fileService.delete(fileId).subscribe();
    this.uploadedFiles = this.uploadedFiles.filter(f => f.id !== fileId);
  }

  /**
   * download a file with given id
   * @param fileId identifier of the file
   */
  public downloadFile(fileId: number): void {
    this.fileService.download(fileId);
  }

  /**
   * reload uploaded files
   * @private
   */
  private reloadFileData(): void {
    this.courseService.getAllFiles(this.courseId).subscribe(result => {
      this.uploadedFiles = result;
    });
  }
}
