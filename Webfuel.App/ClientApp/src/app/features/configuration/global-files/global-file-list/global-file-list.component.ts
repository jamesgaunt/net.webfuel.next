import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'global-file-list',
  templateUrl: './global-file-list.component.html',
})
export class GlobalFileListComponent {
  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  globalFileStorageGroupId = '17c28098-375c-4a1a-bc41-43813786ab84'; // This is the ID for the Global File Storage Group
}
