import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { SupportRequestStatusEnum } from "../../../../api/api.enums";
import { SupportRequest, SupportRequestStatus } from "../../../../api/api.types";
import { StaticDataCache } from "../../../../api/static-data.cache";
import { SupportRequestApi } from "../../../../api/support-request.api";
import { ConfirmDialog } from "../../../../shared/dialogs/confirm/confirm.dialog";

@Component({
  template: ''
})
export abstract class SupportRequestComponentBase implements OnInit {

  // Dependency Injection

  protected route = inject(ActivatedRoute);
  protected router = inject(Router);
  protected supportRequestApi = inject(SupportRequestApi);
  protected confirmDialog = inject(ConfirmDialog);
  staticDataCache = inject(StaticDataCache);

  constructor() {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportRequest);
  }

  // Project

  item!: SupportRequest;

  reset(item: SupportRequest) {
    this.item = item;
    this.resetStatus();
  }

  // Support Request Status

  supportRequestStatus: SupportRequestStatus | null = null;

  resetStatus() {
    this.staticDataCache.supportRequestStatus.get({ id: this.item.statusId }).subscribe((result) => {
      this.supportRequestStatus = result;
      this.locked ? this.applyLock() : this.clearLock();
    });
  }

  protected applyLock() { };

  protected clearLock() { };

  get locked() { return this.item.statusId != SupportRequestStatusEnum.ToBeTriaged && this.item.statusId != SupportRequestStatusEnum.OnHold; }

  get referred() { return this.item.statusId == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams; }

  unlock() {
    this.confirmDialog.open({ title: "Unlock Support Request", message: "Are you sure you want to return this support request to triage status?" }).subscribe(() => {
      this.supportRequestApi.unlock({
        id: this.item.id,
      }, { successGrowl: "Support Request Unlocked" }).subscribe((result) => {
        this.reset(result);
      })
    });
  }
}
