import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {RoleAuthService} from '../role-auth.service';
import {FileService} from '../file.service';
import {CourseFileVM} from '../viewmodels/courseFileVM';
import {CourseService} from '../course.service';
import * as FileSaver from 'file-saver';


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
  @Input()
  public isCourseAdmin: boolean;

  /**
   * file that will be uploaded
   */
  public fileToUpload: File;

  /**
   * list of uploaded files in this course
   */
  public uploadedFiles: CourseFileVM[] = [];

  constructor(roleAuthService: RoleAuthService, fileService: FileService, courseService: CourseService) {
    this.fileService = fileService;
    this.courseService = courseService;
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
  public removeFile(fileId: string): void {
    this.fileService.delete(fileId).subscribe(() => {
      this.reloadFileData();
    });
  }

  /**
   * download a file with given id
   * @param fileId identifier of the file
   * @param fileName name of the downloaded file
   */
  public downloadFile(fileId: string, fileName: string): void {
    this.fileService.download(fileId).subscribe(file => {
      FileSaver.saveAs(file, fileName);
    });
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
