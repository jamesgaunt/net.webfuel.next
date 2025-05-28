import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IStaticDataModel, SupportRequest } from 'api/api.types';
import { StaticDataService } from 'core/static-data.service';

@Component({
  selector: 'support-request-printout',
  templateUrl: './support-request-printout.component.html',
})
export class SupportRequestPrintoutComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  staticDataService = inject(StaticDataService);

  constructor() {}

  ngOnInit() {
    this.item = this.route.snapshot.data.supportRequest;
    this.staticDataService.staticData.subscribe((staticData) => {
      this.s = staticData!;
    });
  }

  item!: SupportRequest;

  s!: IStaticDataModel;

  back() {
    this.router.navigate(['support-request/support-request-item', this.item.id]);
  }

  print() {
    window.print();
  }

  single(id: string | null, items: { id: string; name: string }[]): string {
    if (!id) {
      return 'Not Specified';
    }
    const item = items.find((i) => i.id === id);
    return item ? item.name : '';
  }

  multiple(id: string[], items: { id: string; name: string }[]): string {
    if (!id || id.length === 0) {
      return 'Not Specified';
    }
    return id
      .map((i) => {
        const item = items.find((j) => j.id === i);
        return item ? item.name : '';
      })
      .filter((i) => i)
      .join(', ');
  }
}
