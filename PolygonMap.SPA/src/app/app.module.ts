import { ApiService } from './Api.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



import { MatSelectModule } from '@angular/material/select';

import { CovalentLayoutModule } from '@covalent/core/layout';
import { CovalentStepsModule } from '@covalent/core/steps';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { AppComponent } from './app.component';
import { AgmCoreModule } from '@agm/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CovalentDialogsModule } from '@covalent/core/dialogs';
import { CovalentLoadingModule } from '@covalent/core/loading';


import { MatProgressBarModule } from '@angular/material/progress-bar';

import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DeletedialogComponent } from './deletedialog/deletedialog.component';
@NgModule({
  declarations: [
    AppComponent,
    DeletedialogComponent
  ],
  imports: [
    BrowserModule,
    MatIconModule,
    CovalentLoadingModule,
    HttpClientModule,
    FormsModule,
    MatProgressBarModule,
    MatDialogModule,
    ReactiveFormsModule,
    CovalentDialogsModule,
    MatInputModule,
    CovalentLayoutModule,
    CovalentStepsModule,
    MatSidenavModule,
    MatSelectModule,
    MatButtonModule,
    BrowserAnimationsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyC9PnuRk42kbCPMOvsfHpn40r5SoyN38zI',
      libraries: ['places', 'drawing', 'geometry'],

    })
  ],
  providers: [ApiService, { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} }],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  entryComponents: [DeletedialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
