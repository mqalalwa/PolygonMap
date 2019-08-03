import { DeletedialogComponent } from './deletedialog/deletedialog.component';
import { Shape } from './Shape.model';
import { Component, OnInit } from '@angular/core';
import { ApiService } from './Api.service';
import { Polygon } from './polygon.model';
import { MouseEvent, LatLngLiteral } from '@agm/core/map-types';
import { MatDialog } from '@angular/material/dialog';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import html2canvas from 'html2canvas';

declare const google: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent implements OnInit {
  polygon: any;
  map;
  Shapes: Shape[];
  newlng = 35.25868;
  paths: Array<LatLngLiteral>;
  Name;
  hallMap;
  CurrentPolygon: Polygon;
  editmode = false;
  newlat = 31.95229;
  newPolygonMode = false;
  showPolyGon = false;
  ShapeID;
  zoom = 10;

  ngOnInit(): void {
    this.matIcon.addSvgIcon('google',
      this.domSant.bypassSecurityTrustResourceUrl('../assets/google.svg'))
    this.matIcon.addSvgIcon('fb', this.domSant.bypassSecurityTrustResourceUrl('../assets/fb.svg'));

    this.api.getShapes().subscribe((shapes: Shape[]) => {
      this.Shapes = shapes;
    });
  }

  bater() {
    html2canvas(document.getElementById('mappp')).then(canvas => {

      this.saveAs(canvas.toDataURL("image/png"), `canvas.png`)
    });
  }
  saveAs(uri, filename) {
    var link = document.createElement('a');
    if (typeof link.download === 'string') {
      link.href = uri;
      link.download = filename;
      //Firefox requires the link to be in the body
      document.body.appendChild(link);

      //simulate click
      link.click();

      //remove the link when done
      document.body.removeChild(link);
    } else {
      window.open(uri);
    }
  }
  constructor(private domSant: DomSanitizer,
    private matIcon: MatIconRegistry,
    private api: ApiService,
    public dialog: MatDialog
  ) {
  }
  showrecycleBinIcon = false;
  showRecycleBin() {
    this.showrecycleBinIcon = true;
  }
  onMapReady(map) {
    this.map = map;
  }
  getPaths(poly) {
    console.log("get path");
    const vertices = poly.getPaths().getArray()[0];
    let paths = [];
    vertices.getArray().forEach(function (xy, i) {
      // console.log(xy);
      let latLng = {
        lat: xy.lat(),
        lng: xy.lng()
      };
      paths.push(JSON.stringify(latLng));
    });
    return paths;

  }
  Cancel() {
    this.editmode = false;
    this.newPolygonMode = false;
    this.hallMap.setMap(null);
  }

  Save() {
    this.paths = [];
    this.newPolygonMode = false;
    if (!this.editmode) {
      this.api.getPolygonTodraw(this.Name, this.ShapeID, this.newlat, this.newlng).subscribe((polgon: Polygon) => {
        this.CurrentPolygon = polgon;
        let points = JSON.parse(this.CurrentPolygon.points);
        points.map(a => {
          this.paths.push({ lat: a.Latitude, lng: a.Longitude });

        });
        this.showPolyGon = true;
        this.newlat = this.paths[0].lat
        this.newlng = this.paths[0].lng;
        this.zoom = 14;

      });
    } else {
      this.CurrentPolygon.shapeID = this.ShapeID;
      this.api.UpdatePolygon(this.CurrentPolygon.polygonID, this.CurrentPolygon).subscribe((polgon: Polygon) => {
        this.CurrentPolygon = polgon;
        this.paths = [];
        let poi = JSON.parse(polgon.points);
        poi.map(a => {
          this.paths.push({ lat: a.Latitude, lng: a.Longitude });
        });
        this.showPolyGon = true;
        this.editmode = false;
      });
    }

  }

  deleteDialog() {

    this.dialog.open(DeletedialogComponent,
      { data: this.CurrentPolygon.polygonID, disableClose: true }).afterClosed().
      subscribe(a => {
        if (a) {
          this.showPolyGon = false;
        }
      })
  }

  startEdit() {
    this.editmode = true;
    this.Name = this.CurrentPolygon.name;
    this.newlng = this.CurrentPolygon.realLongitude;
    this.newlat = this.CurrentPolygon.realLatitude;
    this.ShapeID = this.CurrentPolygon;
  }

  markerDragEnd(m: marker, $event: MouseEvent) {
    this.newlat = $event.coords.lat;
    this.newlng = $event.coords.lng;
  }

  marker: marker =
    {
      lat: 31.9522,
      lng: 35.2332,
      label: 'A',
      draggable: true
    }


}
// just an interface for type safety.
interface marker {
  lat: number;
  lng: number;
  label?: string;
  draggable: boolean;
}
