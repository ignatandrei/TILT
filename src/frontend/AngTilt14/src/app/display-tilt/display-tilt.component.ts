import { Component, Input, OnInit } from '@angular/core';
import { TILT } from '../classes/TILT';

@Component({
  selector: 'app-display-tilt',
  templateUrl: './display-tilt.component.html',
  styleUrls: ['./display-tilt.component.css']
})
export class DisplayTILTComponent implements OnInit {

  @Input() tilt :TILT|null = null;
  constructor() { }

  ngOnInit(): void {
    
  }

}
