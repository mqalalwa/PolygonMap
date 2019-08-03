import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from './../Api.service';
import { Component, OnInit, Inject } from '@angular/core';
import { TdLoadingService } from '@covalent/core/loading';

@Component({
  selector: 'app-deletedialog',
  templateUrl: './deletedialog.component.html',
  styleUrls: ['./deletedialog.component.scss']
})
export class DeletedialogComponent implements OnInit {

  constructor( private api: ApiService,
    public dialogRef: MatDialogRef<DeletedialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data) { }
    
  value: number = 0;
  delete = true;
  ngOnInit() {
    let interval: number = setInterval(() => {
      this.value = this.value + 10;
      if (this.value > 100) {
        clearInterval(interval);
      }
    }, 1000);
    setTimeout(() => {
      if (this.delete) {
        this.api.deletePolygogn(this.data).subscribe();
        this.dialogRef.close(true);

      }
    }, 12000);



  }
  onNoClick(): void {
    this.dialogRef.close(false);
    this.delete = false;
  }
}
