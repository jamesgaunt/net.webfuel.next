import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportRequest, QuerySupportRequestChangeLog } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { SupportRequestChangeLogApi } from '../../../../api/support-request-change-log.api';
import { UserService } from '../../../../core/user.service';

@Component({
  selector: 'support-request-history',
  templateUrl: './support-request-history.component.html'
})
export class SupportRequestHistoryComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public supportRequestChangeLogApi: SupportRequestChangeLogApi,
    public staticDataCache: StaticDataCache,
    public userService: UserService,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportRequest);
  }

  item!: SupportRequest;

  reset(item: SupportRequest) {
    this.item = item;
  }

  filter(query: QuerySupportRequestChangeLog) {
    query.supportRequestId = this.item.id;
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['support-request/support-request-list']);
  }

}
