import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ITenant } from 'api/api.types';
import { TenantApi } from 'api/tenant.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'tenant-item',
  templateUrl: './tenant-item.component.html'
})
export class TenantItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private tenantApi: TenantApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.tenant);
  }

  item!: ITenant;

  reset(item: ITenant) {
    this.item = item;
    this.formManager.patchValue(item);
  }

  formManager = this.formService.buildManager({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    live: new FormControl(false, { nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.tenantApi.updateTenant(this.formManager.getRawValue(), { successGrowl: "Tenant Updated", errorHandler: this.formManager }).subscribe((result) => {
      this.router.navigate(['developer/tenant-list']);
    });
  }

  cancel() {
    this.router.navigate(['developer/tenant-list']);
  }
}

export const TenantResolver: ResolveFn<ITenant> =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ITenant> => {
    return inject(TenantApi).resolveTenant({ id: route.paramMap.get('id')! });
  };
