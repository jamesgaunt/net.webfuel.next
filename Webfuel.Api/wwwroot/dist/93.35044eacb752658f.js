"use strict";(self.webpackChunkWebfuel_App=self.webpackChunkWebfuel_App||[]).push([[93],{2093:(ne,T,n)=>{n.r(T),n.d(T,{SupportRequestModule:()=>ie});var m=n(6814),S=n(7453),l=n(3399),h=n(9866),p=n(574),t=n(95),Z=n(7003),e=n(9212),g=n(3180),U=n(7089),q=n(356),b=n(9834),R=n(3993),k=n(7147),w=n(1986),x=n(9510),y=n(3773);let D=(()=>{class a{constructor(o,i,r,u){this.router=o,this.supportRequestApi=i,this.staticDataCache=r,this.reportService=u,this.filterForm=new t.cw({number:new t.NI("",{nonNullable:!0}),fromDate:new t.NI(null),toDate:new t.NI(null),statusId:new t.NI(null),title:new t.NI("",{nonNullable:!0})})}resetFilterForm(){this.filterForm.patchValue({number:"",fromDate:null,toDate:null,statusId:null,title:""})}add(){window.open("/external/support-request","_blank")}edit(o){this.router.navigate(["support-request/support-request-item",o.id])}export(){this.supportRequestApi.export(Z.Z.merge(this.filterForm.getRawValue(),{skip:0,take:0})).subscribe(o=>{this.reportService.runReport(o)})}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(l.F0),e.Y36(p.B),e.Y36(g.U),e.Y36(U.r))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["support-request-list"]],decls:37,vars:9,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/support-request/support-request-list"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"header-buttons"],[1,"button","is-primary",3,"click"],[1,"filter-form"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-12"],["type","text","formControlName","title","placeholder","Title",1,"input"],[1,"field","is-4"],["placeholder","Status","formControlName","statusId",3,"dataSource","enableClear"],["placeholder","From Date","formControlName","fromDate",3,"enableClear"],["placeholder","To Date","formControlName","toDate",3,"enableClear"],[1,"is-12","is-right"],[1,"button","is-small",3,"click"],["stateKey","SupportRequestList",3,"dataSource","filterForm"],["name","dateOfRequest","label","Date"],["name","statusId",3,"dataSource"],["name","title"],["name","targetSubmissionDate","label","Target Submission Date"],[3,"canDelete","add","edit"]],template:function(i,r){1&i&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Triage Support Requests"),e.qZA()()(),e.TgZ(7,"div",3)(8,"div",4)(9,"div",5)(10,"h1",6),e._uU(11,"Triage Support Requests"),e.qZA()(),e.TgZ(12,"div",5)(13,"div",7)(14,"button",8),e.NdJ("click",function(){return r.export()}),e._uU(15,"Export"),e.qZA()()()(),e.TgZ(16,"div",9)(17,"form",10)(18,"div",11)(19,"div",12),e._UZ(20,"input",13),e.qZA()(),e.TgZ(21,"div",11)(22,"div",14),e._UZ(23,"dropdown-select",15),e.qZA(),e.TgZ(24,"div",14),e._UZ(25,"date-picker",16),e.qZA(),e.TgZ(26,"div",14),e._UZ(27,"date-picker",17),e.qZA(),e.TgZ(28,"div",18)(29,"button",19),e.NdJ("click",function(){return r.resetFilterForm()}),e._uU(30,"show all"),e.qZA()()()()(),e.TgZ(31,"grid",20),e._UZ(32,"grid-date-column",21)(33,"grid-reference-column",22)(34,"grid-column",23)(35,"grid-date-column",24),e.TgZ(36,"grid-action-column",25),e.NdJ("add",function(){return r.add()})("edit",function(c){return r.edit(c)}),e.qZA()()()),2&i&&(e.xp6(17),e.Q6J("formGroup",r.filterForm),e.xp6(6),e.Q6J("dataSource",r.staticDataCache.supportRequestStatus)("enableClear",!0),e.xp6(2),e.Q6J("enableClear",!0),e.xp6(2),e.Q6J("enableClear",!0),e.xp6(4),e.Q6J("dataSource",r.supportRequestApi)("filterForm",r.filterForm),e.xp6(2),e.Q6J("dataSource",r.staticDataCache.supportRequestStatus),e.xp6(3),e.Q6J("canDelete",!1))},dependencies:[t._Y,t.Fj,t.JJ,t.JL,t.sg,t.u,l.rH,q.B,b.L,R.M,k.I,w.k,x.K,y.a],encapsulation:2})}return a})();var d=n(6765),J=n(8916);let v=(()=>{class a{constructor(){this.route=(0,e.f3M)(l.gz),this.router=(0,e.f3M)(l.F0),this.supportRequestApi=(0,e.f3M)(p.B),this.confirmDialog=(0,e.f3M)(J.Q),this.staticDataCache=(0,e.f3M)(g.U),this.supportRequestStatus=null}ngOnInit(){this.reset(this.route.snapshot.data.supportRequest)}reset(o){this.item=o,this.resetStatus()}resetStatus(){this.staticDataCache.supportRequestStatus.get({id:this.item.statusId}).subscribe(o=>{this.supportRequestStatus=o,this.locked?this.applyLock():this.clearLock()})}applyLock(){}clearLock(){}get locked(){return this.item.statusId!=d.JR.ToBeTriaged&&this.item.statusId!=d.JR.OnHold}get referred(){return this.item.statusId==d.JR.ReferredToNIHRRSSExpertTeams}unlock(){this.confirmDialog.open({title:"Unlock Support Request",message:"Are you sure you want to return this support request to triage status?"}).subscribe(()=>{this.supportRequestApi.unlock({id:this.item.id},{successGrowl:"Support Request Unlocked"}).subscribe(o=>{this.reset(o)})})}static#e=this.\u0275fac=function(i){return new(i||a)};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["ng-component"]],decls:0,vars:0,template:function(i,r){},encapsulation:2})}return a})();var F=n(7816),f=n(4821),L=n(4891),N=n(7333),I=n(6963);function Q(a,s){1&a&&e._uU(0),2&a&&e.hij(" ",s.$implicit.name," ")}function H(a,s){1&a&&e._uU(0),2&a&&e.hij(" ",s.$implicit.name," ")}function M(a,s){if(1&a&&(e.TgZ(0,"div",3)(1,"div",12)(2,"div",4)(3,"label",5),e._uU(4,"Time in hours (0 - 8)"),e.qZA(),e._UZ(5,"input",13),e.qZA(),e.TgZ(6,"div",4)(7,"label",5),e._uU(8,"Pre/Post Award"),e.qZA(),e._UZ(9,"dropdown-select",14),e.qZA()(),e.TgZ(10,"div",4)(11,"label",5),e._uU(12,"Support provided (include all types of support provided)"),e.qZA(),e._UZ(13,"dropdown-multi-select",15),e.qZA(),e.TgZ(14,"div",4)(15,"label",5),e._uU(16,"Description of support provided/requested"),e.qZA(),e._UZ(17,"textarea",16),e.qZA(),e.TgZ(18,"div",4)(19,"label",5),e._uU(20,"Request additional support from another team"),e.qZA(),e.TgZ(21,"dropdown-select",17),e.YNc(22,Q,1,1,"ng-template",null,18,e.W1O)(24,H,1,1,"ng-template",null,19,e.W1O),e.qZA()()()),2&a){const o=e.oxw();e.xp6(9),e.Q6J("dataSource",o.staticDataCache.isPrePostAward),e.xp6(4),e.Q6J("dataSource",o.staticDataCache.supportProvided),e.xp6(8),e.Q6J("dataSource",o.staticDataCache.supportTeam)("enableClear",!0)}}function P(a,s){1&a&&(e.TgZ(0,"div")(1,"div",4)(2,"label",5),e._uU(3,"Reason for triage decision"),e.qZA(),e._UZ(4,"input",20),e.qZA()())}let C=(()=>{class a extends N.M{open(o){return this._open(Y,o)}static#e=this.\u0275fac=(()=>{let o;return function(r){return(o||(o=e.n5z(a)))(r||a)}})();static#t=this.\u0275prov=e.Yz7({token:a,factory:a.\u0275fac})}return a})(),Y=(()=>{class a extends N.X{constructor(o,i,r,u){super(),this.router=o,this.formService=i,this.supportRequestApi=r,this.staticDataCache=u,this.form=new t.cw({id:new t.NI("",{validators:t.kI.required,nonNullable:!0}),statusId:new t.NI(null,{validators:t.kI.required,nonNullable:!0}),supportProvidedIds:new t.NI([],{nonNullable:!0}),description:new t.NI("",{nonNullable:!0}),workTimeInHours:new t.NI(null,{nonNullable:!0}),supportRequestedTeamId:new t.NI(null),isPrePostAwardId:new t.NI(d.a0.PreAward,{nonNullable:!0}),triageNote:new t.NI("",{nonNullable:!0})}),this.form.patchValue({id:this.data.id}),this.staticDataCache.supportProvided.query({skip:0,take:100}).subscribe(c=>{this.form.patchValue({supportProvidedIds:Z.Z.map(Z.Z.filter(c.items,A=>A.default),A=>A.id)})}),this.form.controls.statusId.valueChanges.subscribe(c=>{this.form.controls.workTimeInHours.setValidators(c==d.JR.ReferredToNIHRRSSExpertTeams?[t.kI.required,t.kI.min(0),t.kI.max(8)]:[]),this.form.controls.workTimeInHours.updateValueAndValidity()})}get promoting(){return this.form.value.statusId==d.JR.ReferredToNIHRRSSExpertTeams}save(){this.formService.hasErrors(this.form)||this.supportRequestApi.triage(this.form.getRawValue()).subscribe(o=>{this._closeDialog(o),null!=o.projectId&&this.router.navigateByUrl(`/project/project-item/${o.projectId}`)})}cancel(){this._cancelDialog()}filterStatus(o){o.filters=o.filters||[],o.filters.push({field:"default",op:d.k8.Equal,value:!1})}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(l.F0),e.Y36(f.o),e.Y36(p.B),e.Y36(g.U))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["triage-support-request-dialog"]],features:[e.qOj],decls:16,vars:4,consts:[[1,"form","dialog","is-warning",3,"formGroup"],[1,"dialog-head"],[1,"dialog-body"],[1,"grid"],[1,"field"],[1,"label"],["placeholder","required","formControlName","statusId",3,"dataSource","filter"],["class","grid",4,"ngIf"],[4,"ngIf"],[1,"dialog-buttons"],["type","submit",1,"is-success",3,"click"],["type","button",3,"click"],[1,"flex"],["type","number","min","0","max","8","formControlName","workTimeInHours","placeholder","required",1,"input"],["placeholder","required","formControlName","isPrePostAwardId",3,"dataSource"],["placeholder","optional","formControlName","supportProvidedIds",3,"dataSource"],["formControlName","description","placeholder","required",1,"textarea",2,"height","150px"],["placeholder","optional","formControlName","supportRequestedTeamId",3,"dataSource","enableClear"],["optionTemplate",""],["pickedTemplate",""],["formControlName","triageNote","placeholder","optional",1,"input"]],template:function(i,r){1&i&&(e.TgZ(0,"form",0)(1,"div",1),e._uU(2," Triage Support Request "),e.qZA(),e.TgZ(3,"div",2)(4,"div",3)(5,"div",4)(6,"label",5),e._uU(7,"Please select outcome of triage"),e.qZA(),e.TgZ(8,"dropdown-select",6),e.NdJ("filter",function(c){return r.filterStatus(c)}),e.qZA()(),e.YNc(9,M,26,4,"div",7)(10,P,5,0,"div",8),e.qZA()(),e.TgZ(11,"div",9)(12,"button",10),e.NdJ("click",function(){return r.save()}),e._uU(13,"Save"),e.qZA(),e.TgZ(14,"button",11),e.NdJ("click",function(){return r.cancel()}),e._uU(15,"Cancel"),e.qZA()()()),2&i&&(e.Q6J("formGroup",r.form),e.xp6(8),e.Q6J("dataSource",r.staticDataCache.supportRequestStatus),e.xp6(),e.Q6J("ngIf",r.promoting),e.xp6(),e.Q6J("ngIf",!r.promoting))},dependencies:[m.O5,t._Y,t.Fj,t.wV,t.JJ,t.JL,t.qQ,t.Fd,t.sg,t.u,q.B,I.O],encapsulation:2})}return a})();var G=n(910);let _=(()=>{class a{constructor(o,i){this.route=o,this.router=i}ngOnInit(){this.reset(this.route.snapshot.data.supportRequest)}reset(o){this.item=o}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(l.gz),e.Y36(l.F0))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["support-request-tabs"]],decls:11,vars:3,consts:[[1,"tabs"],["routerLinkActive","is-active"],[3,"routerLink"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0)(1,"ul")(2,"li",1)(3,"a",2),e._uU(4,"Request"),e.qZA()(),e.TgZ(5,"li",1)(6,"a",2),e._uU(7,"Researcher"),e.qZA()(),e.TgZ(8,"li",1)(9,"a",2),e._uU(10,"Files"),e.qZA()()()()),2&i&&(e.xp6(3),e.MGl("routerLink","../../support-request-item/",r.item.id,""),e.xp6(3),e.MGl("routerLink","../../support-request-researcher/",r.item.id,""),e.xp6(3),e.MGl("routerLink","../../support-request-files/",r.item.id,""))},dependencies:[l.rH,l.Od],encapsulation:2})}return a})();function E(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"div",6)(1,"div",35)(2,"button",36),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.triage())}),e._uU(3,"Triage"),e.qZA()()()}}function O(a,s){if(1&a&&(e.TgZ(0,"span"),e._uU(1),e.qZA()),2&a){const o=e.oxw();e.xp6(),e.hij(": ",o.item.triageNote,"")}}function B(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"div",37)(1,"button",38),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.save(!1))}),e._uU(2,"Save"),e.qZA(),e.TgZ(3,"button",38),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.save(!0))}),e._uU(4,"Save & Close"),e.qZA(),e.TgZ(5,"button",39),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.cancel())}),e._uU(6,"Cancel"),e.qZA()()}}function j(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"button",36),e.NdJ("click",function(){e.CHM(o);const r=e.oxw(2);return e.KtG(r.unlock())}),e._uU(1,"Unlock"),e.qZA()}}function V(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"div",37),e.YNc(1,j,2,0,"button",40),e.TgZ(2,"button",39),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.cancel())}),e._uU(3,"Close"),e.qZA()()}if(2&a){const o=e.oxw();e.xp6(),e.Q6J("ngIf",!o.referred)}}let W=(()=>{class a extends v{constructor(o,i,r){super(),this.formService=o,this.alertDialog=i,this.triageSupportRequestDialog=r,this.form=new t.cw({id:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),isThisRequestLinkedToAnExistingProject:new t.NI(!1,{nonNullable:!0}),title:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),isFellowshipId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),nihrApplicationId:new t.NI("",{nonNullable:!0}),proposedFundingStreamName:new t.NI("",{nonNullable:!0}),targetSubmissionDate:new t.NI(null),experienceOfResearchAwards:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),isTeamMembersConsultedId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),isResubmissionId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),briefDescription:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),supportRequested:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),applicationStageId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),applicationStageFreeText:new t.NI("",{nonNullable:!0}),proposedFundingStreamId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),proposedFundingCallTypeId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),howDidYouFindUsId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),howDidYouFindUsFreeText:new t.NI("",{nonNullable:!0}),whoElseIsOnTheStudyTeam:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),isCTUAlreadyInvolvedId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),isCTUAlreadyInvolvedFreeText:new t.NI("",{nonNullable:!0}),professionalBackgroundIds:new t.NI([],{validators:[F.Q.minArrayLength(1)],nonNullable:!0}),professionalBackgroundFreeText:new t.NI("",{nonNullable:!0})})}reset(o){super.reset(o),this.form.patchValue(o),this.form.markAsPristine()}applyLock(){this.form.disable()}clearLock(){this.form.enable()}save(o){this.formService.hasErrors(this.form)||this.supportRequestApi.update(this.form.getRawValue(),{successGrowl:"Support Request Updated"}).subscribe(i=>{this.reset(i),o&&this.router.navigate(["support-request/support-request-list"])})}cancel(){this.reset(this.item),this.router.navigate(["support-request/support-request-list"])}triage(){this.form.pristine?this.triageSupportRequestDialog.open({id:this.item.id}).subscribe(o=>{this.reset(o)}):this.alertDialog.open({title:"Warning",message:"Please save unsaved changes before triaging"})}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(f.o),e.Y36(L.a),e.Y36(C))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["support-request-item"]],features:[e.qOj],decls:102,vars:25,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/support-request/support-request-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],["class","column",4,"ngIf"],[1,"message","is-primary"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-12"],[1,"label"],["type","text","placeholder","required","formControlName","title",1,"input"],[1,"checkbox"],["type","checkbox","formControlName","isThisRequestLinkedToAnExistingProject"],[1,"field","is-6"],["placeholder","required","formControlName","applicationStageId",3,"dataSource","freeTextControl"],["placeholder","required","formControlName","isFellowshipId",3,"dataSource"],["placeholder","required","formControlName","proposedFundingStreamId",3,"dataSource"],["type","text","placeholder","optional","formControlName","proposedFundingStreamName",1,"input"],["placeholder","required","formControlName","proposedFundingCallTypeId",3,"dataSource"],["type","text","placeholder","optional","formControlName","nihrApplicationId","maxlength","64",1,"input"],["placeholder","optional","formControlName","targetSubmissionDate"],["placeholder","required","formControlName","isTeamMembersConsultedId",3,"dataSource"],["placeholder","required","formControlName","isResubmissionId",3,"dataSource"],["freeTextPlaceholder","Please specify which CTU is already involved...","placeholder","required","formControlName","isCTUAlreadyInvolvedId",3,"dataSource","freeTextControl"],["placeholder","required","formControlName","howDidYouFindUsId",3,"dataSource","freeTextControl"],["formControlName","experienceOfResearchAwards","placeholder","required"],["formControlName","briefDescription","placeholder","required"],["formControlName","supportRequested","placeholder","required"],["formControlName","whoElseIsOnTheStudyTeam","placeholder","required","maxlength","2000"],["placeholder","required","formControlName","professionalBackgroundIds",3,"dataSource","enableClear","freeTextControl"],["class","floating-buttons",4,"ngIf"],[1,"header-buttons"],[1,"button","is-warning",3,"click"],[1,"floating-buttons"],[1,"button","is-success",3,"click"],[1,"button",3,"click"],["class","button is-warning",3,"click",4,"ngIf"]],template:function(i,r){1&i&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Support Requests"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"Support Request"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"Support Request"),e.qZA()(),e.YNc(15,E,4,0,"div",8),e.qZA(),e._UZ(16,"support-request-tabs"),e.TgZ(17,"div",9),e._uU(18),e.YNc(19,O,2,1,"span"),e.qZA(),e.TgZ(20,"form",10)(21,"div",11)(22,"div",12)(23,"label",13),e._uU(24,"Working title of the project"),e.qZA(),e._UZ(25,"input",14),e.qZA(),e.TgZ(26,"div",12)(27,"label",13),e._uU(28,"Is this request linked to a current project?"),e.qZA(),e.TgZ(29,"div",15)(30,"label"),e._UZ(31,"input",16),e._uU(32," Please tick this checkbox if you have already submitted a request in relation to this project."),e.qZA()()(),e.TgZ(33,"div",17)(34,"label",13),e._uU(35,"Stage of application"),e.qZA(),e._UZ(36,"dropdown-select",18),e.qZA(),e.TgZ(37,"div",17)(38,"label",13),e._uU(39,"Is this application for a fellowship?"),e.qZA(),e._UZ(40,"dropdown-select",19),e.qZA(),e.TgZ(41,"div",17)(42,"label",13),e._uU(43,"Proposed funding stream"),e.qZA(),e._UZ(44,"dropdown-select",20),e.qZA(),e.TgZ(45,"div",17)(46,"label",13),e._uU(47,"Funding stream name / specialist call if known"),e.qZA(),e._UZ(48,"input",21),e.qZA(),e.TgZ(49,"div",17)(50,"label",13),e._uU(51,"Type of call"),e.qZA(),e._UZ(52,"dropdown-select",22),e.qZA(),e.TgZ(53,"div",17)(54,"label",13),e._uU(55,"NIHR application ID if known"),e.qZA(),e._UZ(56,"input",23),e.qZA(),e.TgZ(57,"div",17)(58,"label",13),e._uU(59,"Target submission date (if known)"),e.qZA(),e._UZ(60,"date-picker",24),e.qZA(),e.TgZ(61,"div",17)(62,"label",13),e._uU(63,"Have your team members been consulted for support?"),e.qZA(),e._UZ(64,"dropdown-select",25),e.qZA(),e.TgZ(65,"div",17)(66,"label",13),e._uU(67,"Has the application or similar been submitted to a funder before?"),e.qZA(),e._UZ(68,"dropdown-select",26),e.qZA(),e.TgZ(69,"div",17)(70,"label",13),e._uU(71,"Have you already got a CTU involved and if so which one?"),e.qZA(),e._UZ(72,"dropdown-select",27),e.qZA(),e.TgZ(73,"div",17)(74,"label",13),e._uU(75,"How did you hear about our hub?"),e.qZA(),e._UZ(76,"dropdown-select",28),e.qZA(),e.TgZ(77,"div",12)(78,"label",13),e._uU(79,"What is your (or the CIs) experience of securing funding awards?"),e.qZA(),e._UZ(80,"text-area",29),e.qZA(),e.TgZ(81,"div",12)(82,"label",13),e._uU(83," Please add a brief description of your proposed study. Where possible include an outline of research question, aims, need and methods (including analysis), potential impact. [max 500 words] "),e.qZA(),e._UZ(84,"text-area",30),e.qZA(),e.TgZ(85,"div",12)(86,"label",13),e._uU(87," What support are you looking for from RSS? Eg funding advice, methods advice, PPIE and EDI advice, CTU support, training in methods for grants? "),e.qZA(),e._UZ(88,"text-area",31),e.qZA(),e.TgZ(89,"div",12)(90,"label",13),e._uU(91," Who else is on your study team and what is their expertise? "),e.qZA(),e._UZ(92,"text-area",32),e.qZA(),e.TgZ(93,"div",12)(94,"label",13),e._uU(95,"What is the professional background of all team/applicants? (select all that apply)"),e.qZA(),e._UZ(96,"dropdown-multi-select",33),e.qZA()(),e.YNc(97,B,7,0,"div",34)(98,V,4,1,"div",34),e.qZA(),e.TgZ(99,"p"),e._uU(100),e.ALo(101,"date"),e.qZA()()),2&i&&(e.xp6(8),e.MGl("routerLink","/support-request/support-request-item/",r.item.id,""),e.xp6(7),e.Q6J("ngIf",!r.locked),e.xp6(3),e.hij(" ",null==r.supportRequestStatus?null:r.supportRequestStatus.name," "),e.xp6(),e.um2(19,r.item.triageNote?19:-1),e.xp6(),e.Q6J("formGroup",r.form),e.xp6(16),e.Q6J("dataSource",r.staticDataCache.applicationStage)("freeTextControl",r.form.controls.applicationStageFreeText),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.isFellowship),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.fundingStream),e.xp6(8),e.Q6J("dataSource",r.staticDataCache.fundingCallType),e.xp6(12),e.Q6J("dataSource",r.staticDataCache.isTeamMembersConsulted),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.isResubmission),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.isCTUAlreadyInvolved)("freeTextControl",r.form.controls.isCTUAlreadyInvolvedFreeText),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.howDidYouFindUs)("freeTextControl",r.form.controls.howDidYouFindUsFreeText),e.xp6(20),e.Q6J("dataSource",r.staticDataCache.professionalBackground)("enableClear",!0)("freeTextControl",r.form.controls.professionalBackgroundFreeText),e.xp6(),e.Q6J("ngIf",!r.locked),e.xp6(),e.Q6J("ngIf",r.locked),e.xp6(2),e.hij("Created ",e.xi3(101,22,r.item.createdAt,"dd MMM yy HH:mm"),""))},dependencies:[m.O5,t._Y,t.Fj,t.Wl,t.JJ,t.JL,t.nD,t.sg,t.u,l.rH,G.U,q.B,I.O,b.L,_,m.uU],encapsulation:2})}return a})();var K=n(4204);let X=(()=>{class a extends v{constructor(o,i){super(),this.formService=o,this.supportRequestApi=i,this.form=new t.cw({})}cancel(){this.reset(this.item),this.router.navigate(["support-request/support-request-list"])}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(f.o),e.Y36(p.B))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["support-request-files"]],features:[e.qOj],decls:19,vars:4,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/support-request/support-request-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"message","is-primary"],[3,"fileStorageGroupId","locked"]],template:function(i,r){1&i&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Support Requests"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"Support Request"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"Support Request"),e.qZA()()(),e._UZ(15,"support-request-tabs"),e.TgZ(16,"div",8),e._uU(17),e.qZA(),e._UZ(18,"file-browser",9),e.qZA()),2&i&&(e.xp6(8),e.MGl("routerLink","/support-request/support-request-item/",r.item.id,""),e.xp6(9),e.Oqu(null==r.supportRequestStatus?null:r.supportRequestStatus.name),e.xp6(),e.Q6J("fileStorageGroupId",r.item.fileStorageGroupId)("locked",r.locked))},dependencies:[l.rH,K.u,_],encapsulation:2})}return a})();var z=n(649);function $(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"div",45)(1,"button",46),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.save(!1))}),e._uU(2,"Save"),e.qZA(),e.TgZ(3,"button",46),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.save(!0))}),e._uU(4,"Save & Close"),e.qZA(),e.TgZ(5,"button",47),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.cancel())}),e._uU(6,"Cancel"),e.qZA()()}}function ee(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"button",49),e.NdJ("click",function(){e.CHM(o);const r=e.oxw(2);return e.KtG(r.unlock())}),e._uU(1,"Unlock"),e.qZA()}}function te(a,s){if(1&a){const o=e.EpF();e.TgZ(0,"div",45),e.YNc(1,ee,2,0,"button",48),e.TgZ(2,"button",47),e.NdJ("click",function(){e.CHM(o);const r=e.oxw();return e.KtG(r.cancel())}),e._uU(3,"Close"),e.qZA()()}if(2&a){const o=e.oxw();e.xp6(),e.Q6J("ngIf",!o.referred)}}let re=(()=>{class a extends v{constructor(o,i){super(),this.formService=o,this.supportRequestApi=i,this.form=new t.cw({id:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),teamContactTitle:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),teamContactFirstName:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),teamContactLastName:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),teamContactEmail:new t.NI("",{validators:[t.kI.required,t.kI.email],nonNullable:!0}),teamContactRoleId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),teamContactRoleFreeText:new t.NI("",{nonNullable:!0}),teamContactMailingPermission:new t.NI(!1,{validators:[t.kI.requiredTrue],nonNullable:!0}),teamContactPrivacyStatementRead:new t.NI(!1,{validators:[t.kI.requiredTrue],nonNullable:!0}),leadApplicantTitle:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantFirstName:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantLastName:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantEmail:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantJobRole:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantCareerStage:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantOrganisationTypeId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),leadApplicantOrganisation:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantDepartment:new t.NI("",{nonNullable:!0}),leadApplicantAddressLine1:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantAddressLine2:new t.NI("",{nonNullable:!0}),leadApplicantAddressTown:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantAddressCounty:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantAddressCountry:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantAddressPostcode:new t.NI("",{validators:[t.kI.required],nonNullable:!0}),leadApplicantORCID:new t.NI("",{nonNullable:!0}),isLeadApplicantNHSId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),leadApplicantAgeRangeId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),leadApplicantGenderId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0}),leadApplicantEthnicityId:new t.NI(null,{validators:[t.kI.required],nonNullable:!0})})}reset(o){super.reset(o),this.form.patchValue(o),this.form.markAsPristine()}applyLock(){this.form.disable()}clearLock(){this.form.enable()}save(o){this.formService.hasErrors(this.form)||this.supportRequestApi.updateResearcher(this.form.getRawValue(),{successGrowl:"Support Request Updated"}).subscribe(i=>{this.reset(i),o&&this.router.navigate(["support-request/support-request-list"])})}cancel(){this.reset(this.item),this.router.navigate(["support-request/support-request-list"])}static#e=this.\u0275fac=function(i){return new(i||a)(e.Y36(f.o),e.Y36(p.B))};static#t=this.\u0275cmp=e.Xpm({type:a,selectors:[["support-request-researcher"]],features:[e.qOj],decls:149,vars:18,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/support-request/support-request-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"message","is-primary"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-4"],[1,"label"],["placeholder","required","formControlName","teamContactTitle",3,"dataSource"],["type","text","placeholder","required","formControlName","teamContactFirstName",1,"input"],["type","text","placeholder","required","formControlName","teamContactLastName",1,"input"],["type","text","placeholder","required","formControlName","teamContactEmail",1,"input"],["placeholder","required","formControlName","teamContactRoleId",3,"dataSource","freeTextControl"],[1,"field","is-12"],[1,"checkbox"],["type","checkbox","formControlName","teamContactMailingPermission"],["type","checkbox","formControlName","teamContactPrivacyStatementRead"],["placeholder","required","formControlName","leadApplicantTitle",3,"dataSource"],["type","text","placeholder","required","formControlName","leadApplicantFirstName",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantLastName",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantEmail","maxlength","64",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantJobRole",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantCareerStage","maxlength","64",1,"input"],["placeholder","required","formControlName","leadApplicantOrganisationTypeId",3,"dataSource"],["type","text","placeholder","required","formControlName","leadApplicantOrganisation",1,"input"],["type","text","placeholder","optional","formControlName","leadApplicantDepartment",1,"input"],["type","text","placeholder","optional","formControlName","leadApplicantORCID",1,"input"],[1,"field","is-8"],["placeholder","required","formControlName","isLeadApplicantNHSId",3,"dataSource"],[1,"is-6"],["type","text","placeholder","required","formControlName","leadApplicantAddressLine1",1,"input"],["type","text","placeholder","optional","formControlName","leadApplicantAddressLine2",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantAddressTown",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantAddressCounty",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantAddressCountry",1,"input"],["type","text","placeholder","required","formControlName","leadApplicantAddressPostcode",1,"input"],["placeholder","required","formControlName","leadApplicantAgeRangeId",3,"dataSource"],["placeholder","required","formControlName","leadApplicantGenderId",3,"dataSource"],["placeholder","required","formControlName","leadApplicantEthnicityId",3,"dataSource"],["class","floating-buttons",4,"ngIf"],[1,"floating-buttons"],[1,"button","is-success",3,"click"],[1,"button",3,"click"],["class","button is-warning",3,"click",4,"ngIf"],[1,"button","is-warning",3,"click"]],template:function(i,r){1&i&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Support Requests"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"Support Request"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"Support Request"),e.qZA()()(),e._UZ(15,"support-request-tabs"),e.TgZ(16,"div",8),e._uU(17),e.qZA(),e.TgZ(18,"form",9)(19,"h2"),e._uU(20,"Contact Details"),e.qZA(),e.TgZ(21,"div",10)(22,"div",11)(23,"label",12),e._uU(24,"Title"),e.qZA(),e._UZ(25,"dropdown-text-input",13),e.qZA(),e.TgZ(26,"div",11)(27,"label",12),e._uU(28,"First name"),e.qZA(),e._UZ(29,"input",14),e.qZA(),e.TgZ(30,"div",11)(31,"label",12),e._uU(32,"Last name"),e.qZA(),e._UZ(33,"input",15),e.qZA(),e.TgZ(34,"div",11)(35,"label",12),e._uU(36,"Email"),e.qZA(),e._UZ(37,"input",16),e.qZA(),e.TgZ(38,"div",11)(39,"label",12),e._uU(40,"Role in application"),e.qZA(),e._UZ(41,"dropdown-select",17),e.qZA(),e.TgZ(42,"div",18)(43,"div",19)(44,"label"),e._UZ(45,"input",20),e._uU(46," Please tick this checkbox to confirm you give us permission to contact you via email."),e.qZA()()(),e.TgZ(47,"div",18)(48,"div",19)(49,"label"),e._UZ(50,"input",21),e._uU(51," Please tick this checkbox to confirm you have read our privacy statement."),e.qZA()()()(),e.TgZ(52,"h2"),e._uU(53,"Chief Investigator Details"),e.qZA(),e.TgZ(54,"div",10)(55,"div",11)(56,"label",12),e._uU(57,"Title"),e.qZA(),e._UZ(58,"dropdown-text-input",22),e.qZA(),e.TgZ(59,"div",11)(60,"label",12),e._uU(61,"First name"),e.qZA(),e._UZ(62,"input",23),e.qZA(),e.TgZ(63,"div",11)(64,"label",12),e._uU(65,"Last name"),e.qZA(),e._UZ(66,"input",24),e.qZA(),e.TgZ(67,"div",11)(68,"label",12),e._uU(69,"Email"),e.qZA(),e._UZ(70,"input",25),e.qZA(),e.TgZ(71,"div",11)(72,"label",12),e._uU(73,"Job Role"),e.qZA(),e._UZ(74,"input",26),e.qZA(),e.TgZ(75,"div",11)(76,"label",12),e._uU(77,"Career Stage"),e.qZA(),e._UZ(78,"input",27),e.qZA(),e.TgZ(79,"div",11)(80,"label",12),e._uU(81,"Organisation Type"),e.qZA(),e._UZ(82,"dropdown-select",28),e.qZA(),e.TgZ(83,"div",11)(84,"label",12),e._uU(85,"Organisation"),e.qZA(),e._UZ(86,"input",29),e.qZA(),e.TgZ(87,"div",11)(88,"label",12),e._uU(89,"Department"),e.qZA(),e._UZ(90,"input",30),e.qZA(),e.TgZ(91,"div",11)(92,"label",12),e._uU(93,"ORCID"),e.qZA(),e._UZ(94,"input",31),e.qZA(),e.TgZ(95,"div",32)(96,"label",12),e._uU(97," Is the chief investigator an NHS employee seeking RSS support for the first time?\xa0 "),e.qZA(),e._UZ(98,"dropdown-select",33),e.qZA()(),e.TgZ(99,"div",10)(100,"div",34)(101,"h3"),e._uU(102,"Work Address"),e.qZA(),e.TgZ(103,"div",10)(104,"div",18)(105,"label",12),e._uU(106,"Line 1"),e.qZA(),e._UZ(107,"input",35),e.qZA(),e.TgZ(108,"div",18)(109,"label",12),e._uU(110,"Line 2"),e.qZA(),e._UZ(111,"input",36),e.qZA(),e.TgZ(112,"div",18)(113,"label",12),e._uU(114,"Town/City"),e.qZA(),e._UZ(115,"input",37),e.qZA(),e.TgZ(116,"div",18)(117,"label",12),e._uU(118,"County"),e.qZA(),e._UZ(119,"input",38),e.qZA(),e.TgZ(120,"div",18)(121,"label",12),e._uU(122,"Country"),e.qZA(),e._UZ(123,"input",39),e.qZA(),e.TgZ(124,"div",18)(125,"label",12),e._uU(126,"Postcode"),e.qZA(),e._UZ(127,"input",40),e.qZA()()(),e.TgZ(128,"div",34)(129,"h3"),e._uU(130,"Enthnicity, Diversity & Inclusion"),e.qZA(),e.TgZ(131,"div",10)(132,"div",18)(133,"label",12),e._uU(134,"Age Range"),e.qZA(),e._UZ(135,"dropdown-select",41),e.qZA(),e.TgZ(136,"div",18)(137,"label",12),e._uU(138,"Gender"),e.qZA(),e._UZ(139,"dropdown-select",42),e.qZA(),e.TgZ(140,"div",18)(141,"label",12),e._uU(142,"Ethnicity"),e.qZA(),e._UZ(143,"dropdown-select",43),e.qZA()()()(),e.YNc(144,$,7,0,"div",44)(145,te,4,1,"div",44),e.qZA(),e.TgZ(146,"p"),e._uU(147),e.ALo(148,"date"),e.qZA()()),2&i&&(e.xp6(8),e.MGl("routerLink","/support-request/support-request-item/",r.item.id,""),e.xp6(9),e.Oqu(null==r.supportRequestStatus?null:r.supportRequestStatus.name),e.xp6(),e.Q6J("formGroup",r.form),e.xp6(7),e.Q6J("dataSource",r.staticDataCache.title),e.xp6(16),e.Q6J("dataSource",r.staticDataCache.researcherRole)("freeTextControl",r.form.controls.teamContactRoleFreeText),e.xp6(17),e.Q6J("dataSource",r.staticDataCache.title),e.xp6(24),e.Q6J("dataSource",r.staticDataCache.researcherOrganisationType),e.xp6(16),e.Q6J("dataSource",r.staticDataCache.isLeadApplicantNHS),e.xp6(37),e.Q6J("dataSource",r.staticDataCache.ageRange),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.gender),e.xp6(4),e.Q6J("dataSource",r.staticDataCache.ethnicity),e.xp6(),e.Q6J("ngIf",!r.locked),e.xp6(),e.Q6J("ngIf",r.locked),e.xp6(2),e.hij("Created ",e.xi3(148,15,r.item.createdAt,"dd MMM yy HH:mm"),""))},dependencies:[m.O5,t._Y,t.Fj,t.Wl,t.JJ,t.JL,t.nD,t.sg,t.u,l.rH,q.B,z._,_,m.uU],encapsulation:2})}return a})();const oe=[{path:"support-request-list",component:D,data:{activeSideMenu:"Triage"}},{path:"support-request-item/:id",component:W,resolve:{supportRequest:p.B.supportRequestResolver("id")},canDeactivate:[h.u.isPristine()],data:{activeSideMenu:"Triage"}},{path:"support-request-researcher/:id",component:re,resolve:{supportRequest:p.B.supportRequestResolver("id")},canDeactivate:[h.u.isPristine()],data:{activeSideMenu:"Triage"}},{path:"support-request-files/:id",component:X,resolve:{supportRequest:p.B.supportRequestResolver("id")},canDeactivate:[h.u.isPristine()],data:{activeSideMenu:"Triage"}}];let ae=(()=>{class a{static#e=this.\u0275fac=function(i){return new(i||a)};static#t=this.\u0275mod=e.oAB({type:a});static#r=this.\u0275inj=e.cJS({imports:[l.Bz.forChild(oe),l.Bz]})}return a})(),ie=(()=>{class a{static#e=this.\u0275fac=function(i){return new(i||a)};static#t=this.\u0275mod=e.oAB({type:a});static#r=this.\u0275inj=e.cJS({providers:[C],imports:[m.ez,S.m,ae]})}return a})()}}]);