import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IQueryTenantDomain, ITenant, ITenantDomain } from 'api/api.types';
import { TenantDomainApi } from '../../../../api/tenant-domain.api';
import { DialogService } from '../../../../core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import _ from '../../../../shared/underscore';
import { FormService } from '../../../../core/form.service';
import { DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'tenant-domains',
  templateUrl: './tenant-domains.component.html'
})
export class TenantDomainsComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private dialogService: DialogService,
    private tenantDomainApi: TenantDomainApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.tenant);
  }

  item!: ITenant;

  reset(item: ITenant) {
    this.item = item;
  }

  filterForm = new FormGroup({
    search: new FormControl('')
  });

  dataSource = new DataSource<ITenantDomain, IQueryTenantDomain>({
    fetch: (query) => this.tenantDomainApi.queryTenantDomain(_.merge(query, { tenantId: this.item.id })),
    filterGroup: this.filterForm
  });

  // Add

  @ViewChild('addDialog', { static: true }) addDialog!: TemplateRef<any>;

  addForm = this.formService.buildManager({
    domain: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    redirectTo: new FormControl('', { validators: [], nonNullable: true }),
  })

  addDialogRef: DialogRef<unknown, unknown> | null = null;

  add() {
    this.addForm.formGroup.reset({ domain: "", redirectTo: "" });
    this.addDialogRef = this.dialogService.open(this.addDialog);
  }

  doAdd() {
    this.tenantDomainApi.createTenantDomain(_.merge({ tenantId: this.item.id }, this.addForm.getRawValue())).subscribe((result) => {
      this.dataSource.fetch();
      this.addDialogRef?.close();
    });
  }

  cancelAdd() {
    this.addDialogRef?.close();
  }

  // Edit

  @ViewChild('editDialog', { static: true }) editDialog!: TemplateRef<any>;

  editForm = this.formService.buildManager({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    domain: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    redirectTo: new FormControl('', { validators: [], nonNullable: true }),
  })

  editDialogRef: DialogRef<unknown, unknown> | null = null;

  edit(item: ITenantDomain) {
    this.editForm.formGroup.reset(item);
    this.editDialogRef = this.dialogService.open(this.editDialog);
  }

  doEdit() {
    this.tenantDomainApi.updateTenantDomain(_.merge({ tenantId: this.item.id }, this.editForm.getRawValue())).subscribe((result) => {
      this.dataSource.fetch();
      this.editDialogRef?.close();
    });
  }

  cancelEdit() {
    this.editDialogRef?.close();
  }

  // Delete

  delete(item: ITenantDomain) {
    this.dialogService.confirmDelete({
      title: item.domain,
      confirmedCallback: () => {
        this.tenantDomainApi.deleteTenantDomain({ id: item.id }, { successGrowl: "Tenant Domain Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
