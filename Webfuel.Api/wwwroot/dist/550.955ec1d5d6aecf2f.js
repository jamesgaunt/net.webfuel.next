"use strict";(self.webpackChunkWebfuel_App=self.webpackChunkWebfuel_App||[]).push([[550],{7550:(q,A,a)=>{a.r(A),a.d(A,{UserModule:()=>ve});var s=a(6814),_=a(7453),l=a(3399),e=a(9212),r=a(95),m=a(7333),d=a(4821),g=a(5588),p=a(4431),c=a(3180),Z=a(356),f=a(649);let U=(()=>{class i extends m.M{open(){return this._open(T,void 0)}static#e=this.\u0275fac=(()=>{let t;return function(n){return(t||(t=e.n5z(i)))(n||i)}})();static#t=this.\u0275prov=e.Yz7({token:i,factory:i.\u0275fac})}return i})(),T=(()=>{class i extends m.X{constructor(t,o,n,u){super(),this.formService=t,this.userApi=o,this.userGroupApi=n,this.staticDataCache=u,this.form=new r.cw({email:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),title:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),firstName:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),lastName:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),userGroupId:new r.NI("",{validators:[r.kI.required],nonNullable:!0})})}save(){this.formService.hasErrors(this.form)||this.userApi.create(this.form.getRawValue(),{successGrowl:"User Created"}).subscribe(t=>{this._closeDialog(t)})}cancel(){this._cancelDialog()}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(d.o),e.Y36(g.W),e.Y36(p.Q),e.Y36(c.U))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["create-user-dialog"]],features:[e.qOj],decls:30,vars:3,consts:[[1,"form","dialog",3,"formGroup"],[1,"dialog-head"],[1,"dialog-body"],[1,"grid"],[1,"field"],[1,"label"],["type","text","placeholder","required","formControlName","email",1,"input"],["placeholder","required","formControlName","title",3,"dataSource"],["type","text","placeholder","required","formControlName","firstName",1,"input"],["type","text","placeholder","required","formControlName","lastName",1,"input"],["placeholder","required","formControlName","userGroupId",3,"dataSource"],[1,"dialog-buttons"],["type","submit",1,"is-success",3,"click"],["type","button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"form",0)(1,"div",1),e._uU(2," Create User "),e.qZA(),e.TgZ(3,"div",2)(4,"div",3)(5,"div",4)(6,"label",5),e._uU(7,"Email"),e.qZA(),e._UZ(8,"input",6),e.qZA(),e.TgZ(9,"div",4)(10,"label",5),e._uU(11,"Title"),e.qZA(),e._UZ(12,"dropdown-text-input",7),e.qZA(),e.TgZ(13,"div",4)(14,"label",5),e._uU(15,"First Name"),e.qZA(),e._UZ(16,"input",8),e.qZA(),e.TgZ(17,"div",4)(18,"label",5),e._uU(19,"Last Name"),e.qZA(),e._UZ(20,"input",9),e.qZA(),e.TgZ(21,"div",4)(22,"label",5),e._uU(23,"User Group"),e.qZA(),e._UZ(24,"dropdown-select",10),e.qZA()()(),e.TgZ(25,"div",11)(26,"button",12),e.NdJ("click",function(){return n.save()}),e._uU(27,"Add"),e.qZA(),e.TgZ(28,"button",13),e.NdJ("click",function(){return n.cancel()}),e._uU(29,"Cancel"),e.qZA()()()),2&o&&(e.Q6J("formGroup",n.form),e.xp6(12),e.Q6J("dataSource",n.staticDataCache.title),e.xp6(12),e.Q6J("dataSource",n.userGroupApi))},dependencies:[r._Y,r.Fj,r.JJ,r.JL,r.sg,r.u,Z.B,f._],encapsulation:2})}return i})();var b=a(8819),w=a(7089),y=a(3993),I=a(7147),S=a(1986),E=a(3773);let M=(()=>{class i{constructor(t,o,n,u,h,C){this.router=t,this.createUserDialog=o,this.confirmDeleteDialog=n,this.userApi=u,this.userGroupApi=h,this.reportService=C}add(){this.createUserDialog.open()}edit(t){this.router.navigate(["user/user-item",t.id])}delete(t){this.confirmDeleteDialog.open({title:"User"}).subscribe(()=>{this.userApi.delete({id:t.id},{successGrowl:"User Deleted"}).subscribe()})}export(){this.userApi.export({skip:0,take:0}).subscribe(t=>{this.reportService.runReport(t)})}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.F0),e.Y36(U),e.Y36(b.h),e.Y36(g.W),e.Y36(p.Q),e.Y36(w.r))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-list"]],decls:21,vars:3,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/user/user-list"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"header-buttons"],[1,"button","is-primary",3,"click"],[3,"dataSource","search"],["name","fullName","label","Name"],["name","email"],["name","userGroupId",3,"dataSource"],[3,"add","edit","delete"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Users"),e.qZA()()(),e.TgZ(7,"div",3)(8,"div",4)(9,"div",5)(10,"h1",6),e._uU(11,"Users"),e.qZA()(),e.TgZ(12,"div",5)(13,"div",7)(14,"button",8),e.NdJ("click",function(){return n.export()}),e._uU(15,"Export"),e.qZA()()()(),e.TgZ(16,"grid",9),e._UZ(17,"grid-column",10)(18,"grid-column",11)(19,"grid-reference-column",12),e.TgZ(20,"grid-action-column",13),e.NdJ("add",function(){return n.add()})("edit",function(h){return n.edit(h)})("delete",function(h){return n.delete(h)}),e.qZA()()()),2&o&&(e.xp6(16),e.Q6J("dataSource",n.userApi)("search",!0),e.xp6(3),e.Q6J("dataSource",n.userGroupApi))},dependencies:[l.rH,y.M,I.I,S.k,E.a],encapsulation:2})}return i})();var P=a(5770),O=a(4536);let x=(()=>{class i extends m.M{open(t){return this._open(R,t)}static#e=this.\u0275fac=(()=>{let t;return function(n){return(t||(t=e.n5z(i)))(n||i)}})();static#t=this.\u0275prov=e.Yz7({token:i,factory:i.\u0275fac})}return i})(),R=(()=>{class i extends m.X{constructor(t,o,n){super(),this.formService=t,this.growlService=o,this.userLoginApi=n,this.form=new r.cw({userId:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),newPassword:new r.NI("",{validators:[r.kI.required],nonNullable:!0})}),this.form.patchValue({userId:this.data.id})}save(){this.formService.hasErrors(this.form)||this.userLoginApi.updatePassword(this.form.getRawValue()).subscribe(()=>{this.growlService.growlSuccess("Password changed"),this._closeDialog(!0)})}cancel(){this._cancelDialog()}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(d.o),e.Y36(P.e),e.Y36(O.c))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["update-password-dialog"]],features:[e.qOj],decls:14,vars:1,consts:[[1,"form","dialog",3,"formGroup"],[1,"dialog-head"],[1,"dialog-body"],[1,"grid"],[1,"field"],[1,"label"],["type","password","placeholder","required","formControlName","newPassword",1,"input"],[1,"dialog-buttons"],["type","submit",1,"is-success",3,"click"],["type","button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"form",0)(1,"div",1),e._uU(2," Change Password "),e.qZA(),e.TgZ(3,"div",2)(4,"div",3)(5,"div",4)(6,"label",5),e._uU(7,"New Password"),e.qZA(),e._UZ(8,"input",6),e.qZA()()(),e.TgZ(9,"div",7)(10,"button",8),e.NdJ("click",function(){return n.save()}),e._uU(11,"Change Password"),e.qZA(),e.TgZ(12,"button",9),e.NdJ("click",function(){return n.cancel()}),e._uU(13,"Cancel"),e.qZA()()()),2&o&&e.Q6J("formGroup",n.form)},dependencies:[r._Y,r.Fj,r.JJ,r.JL,r.sg,r.u],encapsulation:2})}return i})();var Q=a(6963),D=a(9834),j=a(6411);let G=(()=>{class i{constructor(t,o){this.route=t,this.router=o}ngOnInit(){this.reset(this.route.snapshot.data.user)}reset(t){this.item=t}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-tabs"]],decls:11,vars:3,consts:[[1,"tabs"],["routerLinkActive","is-active"],[3,"routerLink"]],template:function(o,n){1&o&&(e.TgZ(0,"div",0)(1,"ul")(2,"li",1)(3,"a",2),e._uU(4,"User"),e.qZA()(),e.TgZ(5,"li",1)(6,"a",2),e._uU(7,"Support Teams"),e.qZA()(),e.TgZ(8,"li",1)(9,"a",2),e._uU(10,"Activity"),e.qZA()()()()),2&o&&(e.xp6(3),e.MGl("routerLink","../../user-item/",n.item.id,""),e.xp6(3),e.MGl("routerLink","../../user-support-team/",n.item.id,""),e.xp6(3),e.MGl("routerLink","../../user-activity/",n.item.id,""))},dependencies:[l.rH,l.Od],encapsulation:2})}return i})(),H=(()=>{class i{constructor(t,o,n,u,h,C,N){this.route=t,this.router=o,this.formService=n,this.userApi=u,this.userGroupApi=h,this.staticDataCache=C,this.updatePasswordDialog=N,this.form=new r.cw({id:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),email:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),title:new r.NI("",{nonNullable:!0}),firstName:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),lastName:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),userGroupId:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),rssJobTitle:new r.NI("",{nonNullable:!0}),universityJobTitle:new r.NI("",{nonNullable:!0}),professionalBackground:new r.NI("",{nonNullable:!0}),specialisation:new r.NI("",{nonNullable:!0}),disciplineIds:new r.NI([],{nonNullable:!0}),disciplineFreeText:new r.NI("",{nonNullable:!0}),startDateForRSS:new r.NI(null),endDateForRSS:new r.NI(null),fullTimeEquivalentForRSS:new r.NI(null),siteId:new r.NI(null),disabled:new r.NI(!1,{nonNullable:!0}),hidden:new r.NI(!1,{nonNullable:!0})})}ngOnInit(){this.reset(this.route.snapshot.data.user)}reset(t){this.item=t,this.form.patchValue(t),this.form.markAsPristine()}save(t){this.formService.hasErrors(this.form)||this.userApi.update(this.form.getRawValue(),{successGrowl:"User Updated"}).subscribe(o=>{this.reset(o),t&&this.router.navigate(["user/user-list"])})}cancel(){this.reset(this.item),this.router.navigate(["user/user-list"])}updatePassword(){this.updatePasswordDialog.open(this.item)}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0),e.Y36(d.o),e.Y36(g.W),e.Y36(p.Q),e.Y36(c.U),e.Y36(x))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-item"]],decls:99,vars:15,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/user/user-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"header-buttons"],[1,"button","is-warning",3,"click"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-6"],[1,"label"],["type","text","placeholder","required","formControlName","email",1,"input"],["placeholder","required","formControlName","title",3,"dataSource"],["type","text","placeholder","required","formControlName","firstName",1,"input"],["type","text","placeholder","required","formControlName","lastName",1,"input"],["placeholder","required","formControlName","userGroupId",3,"dataSource"],["type","text","placeholder","required","formControlName","rssJobTitle",1,"input"],["type","text","placeholder","required","formControlName","universityJobTitle",1,"input"],["type","text","placeholder","required","formControlName","professionalBackground",1,"input"],["type","text","placeholder","required","formControlName","specialisation",1,"input"],[1,"field","is-12"],["placeholder","required","formControlName","disciplineIds",3,"dataSource","enableSearch","enableClear","freeTextControl"],["placeholder","optional","formControlName","startDateForRSS",3,"enableClear"],["placeholder","optional","formControlName","endDateForRSS",3,"enableClear"],["type","number","step","0.01","min","0","max","1","placeholder","optional","formControlName","fullTimeEquivalentForRSS",1,"input"],["placeholder","required","formControlName","siteId",3,"dataSource"],[1,"flex","is-6"],[1,"field"],["formControlName","hidden"],["formControlName","disabled"],[1,"floating-buttons"],[1,"button","is-success",3,"click"],[1,"button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Users"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"User"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"User"),e.qZA()(),e.TgZ(15,"div",6)(16,"div",8)(17,"button",9),e.NdJ("click",function(){return n.updatePassword()}),e._uU(18,"Change Password"),e.qZA()()()(),e._UZ(19,"user-tabs"),e.TgZ(20,"form",10)(21,"div",11)(22,"div",12)(23,"label",13),e._uU(24,"Email"),e.qZA(),e._UZ(25,"input",14),e.qZA(),e.TgZ(26,"div",12)(27,"label",13),e._uU(28,"Title"),e.qZA(),e._UZ(29,"dropdown-text-input",15),e.qZA(),e.TgZ(30,"div",12)(31,"label",13),e._uU(32,"First name"),e.qZA(),e._UZ(33,"input",16),e.qZA(),e.TgZ(34,"div",12)(35,"label",13),e._uU(36,"Last name"),e.qZA(),e._UZ(37,"input",17),e.qZA(),e.TgZ(38,"div",12)(39,"label",13),e._uU(40,"User group"),e.qZA(),e._UZ(41,"dropdown-select",18),e.qZA()(),e.TgZ(42,"div",11)(43,"div",12)(44,"label",13),e._uU(45,"RSS job title"),e.qZA(),e._UZ(46,"input",19),e.qZA(),e.TgZ(47,"div",12)(48,"label",13),e._uU(49,"University job title"),e.qZA(),e._UZ(50,"input",20),e.qZA(),e.TgZ(51,"div",12)(52,"label",13),e._uU(53,"Professional background"),e.qZA(),e._UZ(54,"input",21),e.qZA(),e.TgZ(55,"div",12)(56,"label",13),e._uU(57,"Specialisation"),e.qZA(),e._UZ(58,"input",22),e.qZA(),e.TgZ(59,"div",23)(60,"label",13),e._uU(61,"Adviser type / discipline"),e.qZA(),e._UZ(62,"dropdown-multi-select",24),e.qZA()(),e.TgZ(63,"div",11)(64,"div",12)(65,"label",13),e._uU(66,"Start date for RSS"),e.qZA(),e._UZ(67,"date-picker",25),e.qZA(),e.TgZ(68,"div",12)(69,"label",13),e._uU(70,"End date for RSS"),e.qZA(),e._UZ(71,"date-picker",26),e.qZA(),e.TgZ(72,"div",12)(73,"label",13),e._uU(74,"Full time equivalent for RSS [0 - 1]"),e.qZA(),e._UZ(75,"input",27),e.qZA(),e.TgZ(76,"div",12)(77,"label",13),e._uU(78,"Site"),e.qZA(),e._UZ(79,"dropdown-select",28),e.qZA()(),e.TgZ(80,"div",29)(81,"div",30)(82,"label",13),e._uU(83,"Hidden (will not be selectable in drop down lists)"),e.qZA(),e._UZ(84,"toggle-input",31),e.qZA(),e.TgZ(85,"div",12)(86,"label",13),e._uU(87,"Disabled (will not be able to login)"),e.qZA(),e._UZ(88,"toggle-input",32),e.qZA()(),e.TgZ(89,"div",33)(90,"button",34),e.NdJ("click",function(){return n.save(!1)}),e._uU(91,"Save"),e.qZA(),e.TgZ(92,"button",34),e.NdJ("click",function(){return n.save(!0)}),e._uU(93,"Save & Close"),e.qZA(),e.TgZ(94,"button",35),e.NdJ("click",function(){return n.cancel()}),e._uU(95,"Cancel"),e.qZA()()(),e.TgZ(96,"p"),e._uU(97),e.ALo(98,"date"),e.qZA()()),2&o&&(e.xp6(8),e.MGl("routerLink","/user/user-item/",n.item.id,""),e.xp6(12),e.Q6J("formGroup",n.form),e.xp6(9),e.Q6J("dataSource",n.staticDataCache.title),e.xp6(12),e.Q6J("dataSource",n.userGroupApi),e.xp6(21),e.Q6J("dataSource",n.staticDataCache.userDiscipline)("enableSearch",!0)("enableClear",!0)("freeTextControl",n.form.controls.disciplineFreeText),e.xp6(5),e.Q6J("enableClear",!0),e.xp6(4),e.Q6J("enableClear",!0),e.xp6(8),e.Q6J("dataSource",n.staticDataCache.site),e.xp6(18),e.hij("Created ",e.xi3(98,12,n.item.createdAt,"dd MMM yy HH:mm"),""))},dependencies:[r._Y,r.Fj,r.wV,r.JJ,r.JL,r.qQ,r.Fd,r.sg,r.u,l.rH,Z.B,Q.O,f._,D.L,j.g,G,s.uU],encapsulation:2})}return i})();var J=a(7003),W=a(5581),B=a(6147),K=a(9510),X=a(8219);const Y=i=>({id:i});function z(i,v){if(1&i&&(e.TgZ(0,"div"),e._uU(1),e.ALo(2,"async"),e.qZA()),2&i){const t=v.$implicit,o=e.oxw(3);let n;e.xp6(),e.hij(" ",null==(n=e.lcZ(2,1,o.staticDataCache.supportProvided.get(e.VKq(3,Y,t))))?null:n.name," ")}}function V(i,v){if(1&i&&(e.TgZ(0,"div"),e.YNc(1,z,3,5,"div",30),e.qZA()),2&i){const t=e.oxw().$implicit;e.xp6(),e.Q6J("ngForOf",t.projectSupportProvidedIds)}}function $(i,v){if(1&i&&(e.TgZ(0,"div"),e._uU(1),e.ALo(2,"async"),e.qZA()),2&i){const t=e.oxw().$implicit,o=e.oxw();let n;e.xp6(),e.hij(" ",null==(n=e.lcZ(2,1,o.staticDataCache.workActivity.get(e.VKq(3,Y,t.workActivityId))))?null:n.name," ")}}function ee(i,v){if(1&i&&e.YNc(0,V,2,1,"div",29)(1,$,3,5,"div",29),2&i){const t=v.$implicit;e.Q6J("ngIf",t.isProjectActivity),e.xp6(),e.Q6J("ngIf",!t.isProjectActivity)}}function te(i,v){if(1&i&&(e.TgZ(0,"pre",31),e._uU(1),e.qZA()),2&i){const t=v.$implicit;e.xp6(),e.Oqu(t.description)}}function ie(i,v){if(1&i&&(e.TgZ(0,"div")(1,"a",32),e._UZ(2,"i",33),e._uU(3),e.qZA()()),2&i){const t=e.oxw().$implicit;e.xp6(),e.MGl("href","/project/project-support/",t.projectId,"",e.LSH),e.xp6(2),e.hij(" ",t.projectPrefixedNumber,"")}}function re(i,v){if(1&i){const t=e.EpF();e.TgZ(0,"div")(1,"a",34),e.NdJ("click",function(){e.CHM(t);const n=e.oxw().$implicit,u=e.oxw();return e.KtG(u.edit(n))}),e._uU(2,"edit"),e.qZA(),e._uU(3," \xa0 "),e.TgZ(4,"a",35),e.NdJ("click",function(){e.CHM(t);const n=e.oxw().$implicit,u=e.oxw();return e.KtG(u.delete(n))}),e._uU(5,"delete"),e.qZA()()}}function oe(i,v){if(1&i&&e.YNc(0,ie,4,2,"div",29)(1,re,6,0,"div",29),2&i){const t=v.$implicit;e.Q6J("ngIf",t.isProjectActivity),e.xp6(),e.Q6J("ngIf",!t.isProjectActivity)}}let ne=(()=>{class i{constructor(t,o,n,u,h,C,N,Ze,Ue){this.route=t,this.router=o,this.formService=n,this.updateUserActivityDialog=u,this.confirmDeleteDialog=h,this.userApi=C,this.userActivityApi=N,this.staticDataCache=Ze,this.reportService=Ue,this.destroyRef=(0,e.f3M)(e.ktI),this.filterForm=new r.cw({fromDate:new r.NI(null),toDate:new r.NI(null),description:new r.NI("",{nonNullable:!0}),userId:new r.NI(null)})}ngOnInit(){this.reset(this.route.snapshot.data.user)}reset(t){this.item=t,this.filterForm.patchValue({userId:t.id})}resetFilterForm(){this.filterForm.patchValue({fromDate:null,toDate:null,description:""})}cancel(){this.reset(this.item),this.router.navigate(["user/user-list"])}edit(t){this.updateUserActivityDialog.open({userActivity:t})}delete(t){this.confirmDeleteDialog.open({title:"User Activity"}).subscribe(()=>{this.userActivityApi.delete({id:t.id},{successGrowl:"User Activity Deleted"}).subscribe()})}export(){this.userActivityApi.export(J.Z.merge(this.filterForm.getRawValue(),{skip:0,take:0})).subscribe(t=>{this.reportService.runReport(t)})}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0),e.Y36(d.o),e.Y36(W.M),e.Y36(b.h),e.Y36(g.W),e.Y36(B.y),e.Y36(c.U),e.Y36(w.r))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-activity"]],decls:47,vars:6,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/user/user-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"header-buttons"],[1,"button","is-primary",3,"click"],[1,"filter-form"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-3"],["placeholder","From Date","formControlName","fromDate",3,"enableClear"],["placeholder","To Date","formControlName","toDate",3,"enableClear"],[1,"field","is-6"],["type","text","formControlName","description","placeholder","Description",1,"input"],[1,"is-12","is-right"],[1,"button","is-small",3,"click"],[3,"dataSource","filterForm"],["name","date"],["name","workTimeInHours","label","Hours","format","0.2-2"],["name","Activity"],["itemTemplate",""],["name","description"],["justify","right"],[1,"floating-buttons"],[1,"button",3,"click"],[4,"ngIf"],[4,"ngFor","ngForOf"],[1,"grid-pre",2,"padding","0"],["target","_blank",1,"link","is-primary",3,"href"],[1,"fas","fa-external-link"],[1,"link","is-primary",3,"click"],[1,"link","is-danger",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Users"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"User"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"User Activity"),e.qZA()(),e.TgZ(15,"div",6)(16,"div",8)(17,"button",9),e.NdJ("click",function(){return n.export()}),e._uU(18,"Export"),e.qZA()()()(),e._UZ(19,"user-tabs"),e.TgZ(20,"div",10)(21,"form",11)(22,"div",12)(23,"div",13),e._UZ(24,"date-picker",14),e.qZA(),e.TgZ(25,"div",13),e._UZ(26,"date-picker",15),e.qZA(),e.TgZ(27,"div",16),e._UZ(28,"input",17),e.qZA(),e.TgZ(29,"div",18)(30,"button",19),e.NdJ("click",function(){return n.resetFilterForm()}),e._uU(31,"show all"),e.qZA()()()()(),e.TgZ(32,"grid",20),e._UZ(33,"grid-date-column",21)(34,"grid-number-column",22),e.TgZ(35,"grid-column",23),e.YNc(36,ee,2,2,"ng-template",null,24,e.W1O),e.qZA(),e.TgZ(38,"grid-column",25),e.YNc(39,te,2,1,"ng-template",null,24,e.W1O),e.qZA(),e.TgZ(41,"grid-column",26),e.YNc(42,oe,2,2,"ng-template",null,24,e.W1O),e.qZA()(),e.TgZ(44,"div",27)(45,"button",28),e.NdJ("click",function(){return n.cancel()}),e._uU(46,"Close"),e.qZA()()()),2&o&&(e.xp6(8),e.MGl("routerLink","/user/user-item/",n.item.id,""),e.xp6(13),e.Q6J("formGroup",n.filterForm),e.xp6(3),e.Q6J("enableClear",!0),e.xp6(2),e.Q6J("enableClear",!0),e.xp6(6),e.Q6J("dataSource",n.userActivityApi)("filterForm",n.filterForm))},dependencies:[s.sg,s.O5,r._Y,r.Fj,r.JJ,r.JL,r.sg,r.u,l.rH,D.L,y.M,I.I,K.K,X.x,G,s.Ov],encapsulation:2})}return i})(),F=(()=>{class i extends m.M{open(){return this._open(se,void 0)}static#e=this.\u0275fac=(()=>{let t;return function(n){return(t||(t=e.n5z(i)))(n||i)}})();static#t=this.\u0275prov=e.Yz7({token:i,factory:i.\u0275fac})}return i})(),se=(()=>{class i extends m.X{constructor(t,o){super(),this.formService=t,this.userGroupApi=o,this.form=new r.cw({name:new r.NI("",{validators:[r.kI.required],nonNullable:!0})})}save(){this.formService.hasErrors(this.form)||this.userGroupApi.create(this.form.getRawValue(),{successGrowl:"User Group Created"}).subscribe(t=>{this._closeDialog(t)})}cancel(){this._cancelDialog()}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(d.o),e.Y36(p.Q))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["create-user-group-dialog"]],features:[e.qOj],decls:14,vars:1,consts:[[1,"form","dialog",3,"formGroup"],[1,"dialog-head"],[1,"dialog-body"],[1,"grid"],[1,"field"],[1,"label"],["type","text","placeholder","required","formControlName","name",1,"input"],[1,"dialog-buttons"],["type","submit",1,"is-success",3,"click"],["type","button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"form",0)(1,"div",1),e._uU(2," Create User Group "),e.qZA(),e.TgZ(3,"div",2)(4,"div",3)(5,"div",4)(6,"label",5),e._uU(7,"Name"),e.qZA(),e._UZ(8,"input",6),e.qZA()()(),e.TgZ(9,"div",7)(10,"button",8),e.NdJ("click",function(){return n.save()}),e._uU(11,"Add"),e.qZA(),e.TgZ(12,"button",9),e.NdJ("click",function(){return n.cancel()}),e._uU(13,"Cancel"),e.qZA()()()),2&o&&e.Q6J("formGroup",n.form)},dependencies:[r._Y,r.Fj,r.JJ,r.JL,r.sg,r.u],encapsulation:2})}return i})(),ae=(()=>{class i{constructor(t,o,n,u){this.router=t,this.createUserGroupDialog=o,this.confirmDeleteDialog=n,this.userGroupApi=u}add(){this.createUserGroupDialog.open()}edit(t){this.router.navigate(["user/user-group-item",t.id])}delete(t){this.confirmDeleteDialog.open({title:"User Group"}).subscribe(()=>{this.userGroupApi.delete({id:t.id},{successGrowl:"User Group Deleted"}).subscribe()})}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.F0),e.Y36(F),e.Y36(b.h),e.Y36(p.Q))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-group-list"]],decls:16,vars:2,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/configuration/configuration-menu"],["routerLink","/user/user-group-list"],[1,"container"],[1,"title"],[3,"dataSource","search"],["name","name"],[3,"add","edit","delete"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Configuration"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"Users Groups"),e.qZA()()(),e.TgZ(10,"div",4)(11,"h1",5),e._uU(12,"User Groups"),e.qZA(),e.TgZ(13,"grid",6),e._UZ(14,"grid-column",7),e.TgZ(15,"grid-action-column",8),e.NdJ("add",function(){return n.add()})("edit",function(h){return n.edit(h)})("delete",function(h){return n.delete(h)}),e.qZA()()()),2&o&&(e.xp6(13),e.Q6J("dataSource",n.userGroupApi)("search",!0))},dependencies:[l.rH,y.M,I.I,S.k],encapsulation:2})}return i})(),L=(()=>{class i{constructor(t,o){this.route=t,this.router=o}ngOnInit(){this.reset(this.route.snapshot.data.userGroup)}reset(t){this.item=t}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-group-tabs"]],decls:8,vars:2,consts:[[1,"tabs"],["routerLinkActive","is-active"],[3,"routerLink"]],template:function(o,n){1&o&&(e.TgZ(0,"div",0)(1,"ul")(2,"li",1)(3,"a",2),e._uU(4,"User Group"),e.qZA()(),e.TgZ(5,"li",1)(6,"a",2),e._uU(7,"Claims"),e.qZA()()()()),2&o&&(e.xp6(3),e.MGl("routerLink","../../user-group-item/",n.item.id,""),e.xp6(3),e.MGl("routerLink","../../user-group-claims/",n.item.id,""))},dependencies:[l.rH,l.Od],encapsulation:2})}return i})(),ue=(()=>{class i{constructor(t,o,n,u){this.route=t,this.router=o,this.formService=n,this.userGroupApi=u,this.form=new r.cw({id:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),name:new r.NI("",{validators:[r.kI.required],nonNullable:!0})})}ngOnInit(){this.reset(this.route.snapshot.data.userGroup)}reset(t){this.item=t,this.form.patchValue(t),this.form.markAsPristine()}save(t){this.formService.hasErrors(this.form)||this.userGroupApi.update(this.form.getRawValue(),{successGrowl:"User Group Updated"}).subscribe(o=>{this.reset(o),t&&this.router.navigate(["user/user-group-list"])})}cancel(){this.reset(this.item),this.router.navigate(["user/user-group-list"])}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0),e.Y36(d.o),e.Y36(p.Q))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-group-item"]],decls:34,vars:2,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/configuration/configuration-menu"],["routerLink","/user/user-group-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"buttons","is-right"],[1,"form",3,"formGroup"],[1,"grid"],[1,"field"],[1,"label"],["type","text","placeholder","required","formControlName","name",1,"input"],[1,"floating-buttons"],[1,"button","is-success",3,"click"],[1,"button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Configuration"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"User Groups"),e.qZA()(),e.TgZ(10,"li")(11,"a",4),e._uU(12,"User Group"),e.qZA()()(),e.TgZ(13,"div",5)(14,"div",6)(15,"div",7)(16,"h1",8),e._uU(17,"User Group"),e.qZA()(),e.TgZ(18,"div",7),e._UZ(19,"div",9),e.qZA()(),e._UZ(20,"user-group-tabs"),e.TgZ(21,"form",10)(22,"div",11)(23,"div",12)(24,"label",13),e._uU(25,"Name"),e.qZA(),e._UZ(26,"input",14),e.qZA()(),e.TgZ(27,"div",15)(28,"button",16),e.NdJ("click",function(){return n.save(!1)}),e._uU(29,"Save"),e.qZA(),e.TgZ(30,"button",16),e.NdJ("click",function(){return n.save(!0)}),e._uU(31,"Save & Close"),e.qZA(),e.TgZ(32,"button",17),e.NdJ("click",function(){return n.cancel()}),e._uU(33,"Cancel"),e.qZA()()()()),2&o&&(e.xp6(11),e.MGl("routerLink","/user/user-group-item/",n.item.id,""),e.xp6(10),e.Q6J("formGroup",n.form))},dependencies:[r._Y,r.Fj,r.JJ,r.JL,r.sg,r.u,l.rH,L],encapsulation:2})}return i})();var k=a(9866);let le=(()=>{class i{constructor(t,o,n,u){this.route=t,this.router=o,this.formService=n,this.userGroupApi=u,this.form=new r.cw({id:new r.NI("",{validators:[r.kI.required],nonNullable:!0}),canEditUsers:new r.NI(!1,{nonNullable:!0}),canEditUserGroups:new r.NI(!1,{nonNullable:!0}),canEditStaticData:new r.NI(!1,{nonNullable:!0}),canEditReports:new r.NI(!1,{nonNullable:!0}),canUnlockProjects:new r.NI(!1,{nonNullable:!0}),canTriageSupportRequests:new r.NI(!1,{nonNullable:!0})})}ngOnInit(){this.reset(this.route.snapshot.data.userGroup)}reset(t){this.item=t,this.form.patchValue({id:t.id}),this.form.patchValue(t.claims),this.form.markAsPristine()}save(t){this.formService.hasErrors(this.form)||this.userGroupApi.updateClaims(this.form.getRawValue(),{successGrowl:"User Group Updated"}).subscribe(o=>{this.reset(o),t&&this.router.navigate(["user/user-group-list"])})}cancel(){this.reset(this.item),this.router.navigate(["user/user-group-list"])}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0),e.Y36(d.o),e.Y36(p.Q))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-group-claims"]],decls:60,vars:2,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/configuration/configuration-menu"],["routerLink","/user/user-group-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"buttons","is-right"],[1,"form",3,"formGroup"],[1,"flex"],[1,"field","is-6"],[1,"checkbox"],["type","checkbox","formControlName","canEditUsers"],["type","checkbox","formControlName","canEditUserGroups"],["type","checkbox","formControlName","canEditStaticData"],["type","checkbox","formControlName","canUnlockProjects"],["type","checkbox","formControlName","canTriageSupportRequests"],["type","checkbox","formControlName","canEditReports"],[1,"floating-buttons"],[1,"button","is-success",3,"click"],[1,"button",3,"click"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Configuration"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"User Groups"),e.qZA()(),e.TgZ(10,"li")(11,"a",4),e._uU(12,"User Group"),e.qZA()()(),e.TgZ(13,"div",5)(14,"div",6)(15,"div",7)(16,"h1",8),e._uU(17,"User Group"),e.qZA()(),e.TgZ(18,"div",7),e._UZ(19,"div",9),e.qZA()(),e._UZ(20,"user-group-tabs"),e.TgZ(21,"form",10)(22,"div",11)(23,"div",12)(24,"div",13)(25,"label"),e._UZ(26,"input",14),e._uU(27," Can Edit Users"),e.qZA()()(),e.TgZ(28,"div",12)(29,"div",13)(30,"label"),e._UZ(31,"input",15),e._uU(32," Can Edit User Groups"),e.qZA()()(),e.TgZ(33,"div",12)(34,"div",13)(35,"label"),e._UZ(36,"input",16),e._uU(37," Can Edit Static Data"),e.qZA()()(),e.TgZ(38,"div",12)(39,"div",13)(40,"label"),e._UZ(41,"input",17),e._uU(42," Can Unlock Projects"),e.qZA()()(),e.TgZ(43,"div",12)(44,"div",13)(45,"label"),e._UZ(46,"input",18),e._uU(47," Can Triage Support Requests"),e.qZA()()(),e.TgZ(48,"div",12)(49,"div",13)(50,"label"),e._UZ(51,"input",19),e._uU(52," Can Access Reports"),e.qZA()()()(),e.TgZ(53,"div",20)(54,"button",21),e.NdJ("click",function(){return n.save(!1)}),e._uU(55,"Save"),e.qZA(),e.TgZ(56,"button",21),e.NdJ("click",function(){return n.save(!0)}),e._uU(57,"Save & Close"),e.qZA(),e.TgZ(58,"button",22),e.NdJ("click",function(){return n.cancel()}),e._uU(59,"Cancel"),e.qZA()()()()),2&o&&(e.xp6(11),e.MGl("routerLink","/user/user-group-item/",n.item.id,""),e.xp6(10),e.Q6J("formGroup",n.form))},dependencies:[r._Y,r.Wl,r.JJ,r.JL,r.sg,r.u,l.rH,L],encapsulation:2})}return i})();a(8581);var ce=a(817),pe=a(6933);function de(i,v){1&i&&e._UZ(0,"i",14)}function me(i,v){1&i&&e._UZ(0,"i",15)}function _e(i,v){if(1&i){const t=e.EpF();e.TgZ(0,"a",11),e.NdJ("click",function(){const u=e.CHM(t).$implicit,h=e.oxw();return e.KtG(h.toggleSupportTeam(u))}),e.YNc(1,de,1,0,"i",12)(2,me,1,0,"i",13),e._uU(3),e.qZA()}if(2&i){const t=v.$implicit,o=e.oxw();e.ekj("selected",o.isSupportTeamSelected(t)),e.xp6(),e.Q6J("ngIf",!o.isSupportTeamSelected(t)),e.xp6(),e.Q6J("ngIf",o.isSupportTeamSelected(t)),e.xp6(),e.hij(" \xa0\xa0 ",t.name," ")}}let fe=(()=>{class i{constructor(t,o,n,u,h,C,N){this.route=t,this.router=o,this.formService=n,this.userApi=u,this.supportTeamApi=h,this.supportTeamUserApi=C,this.staticDataCache=N,this.form=new r.cw({}),this.supportTeams=[],this.supportTeamUsers=[]}ngOnInit(){this.reset(this.route.snapshot.data.user),this.load()}reset(t){this.item=t,this.form.markAsPristine()}cancel(){this.reset(this.item),this.router.navigate(["user/user-list"])}load(){this.supportTeamApi.query({skip:0,take:100}).subscribe(t=>this.supportTeams=t.items),this.supportTeamUserApi.query({skip:0,take:100,userId:this.item.id,supportTeamId:null}).subscribe(t=>this.supportTeamUsers=t.items)}isSupportTeamSelected(t){return J.Z.some(this.supportTeamUsers,o=>o.supportTeamId==t.id)}toggleSupportTeam(t){t._processing=!0,this.isSupportTeamSelected(t)?this.supportTeamUserApi.delete({userId:this.item.id,supportTeamId:t.id},{successGrowl:"Removed from support team"}).subscribe(()=>{this.load()}):this.supportTeamUserApi.insert({userId:this.item.id,supportTeamId:t.id},{successGrowl:"Added to support team"}).subscribe(()=>{this.load()})}static#e=this.\u0275fac=function(o){return new(o||i)(e.Y36(l.gz),e.Y36(l.F0),e.Y36(d.o),e.Y36(g.W),e.Y36(ce.m),e.Y36(pe.m),e.Y36(c.U))};static#t=this.\u0275cmp=e.Xpm({type:i,selectors:[["user-support-team"]],decls:20,vars:2,consts:[[1,"breadcrumbs"],["routerLink","/home"],["routerLink","/user/user-list"],[3,"routerLink"],[1,"container"],[1,"columns"],[1,"column"],[1,"title"],[1,"header-buttons"],[1,"toggle-list"],[3,"selected","click",4,"ngFor","ngForOf"],[3,"click"],["class","far fa-square",4,"ngIf"],["class","far fa-check-square",4,"ngIf"],[1,"far","fa-square"],[1,"far","fa-check-square"]],template:function(o,n){1&o&&(e.TgZ(0,"ul",0)(1,"li")(2,"a",1),e._uU(3,"Home"),e.qZA()(),e.TgZ(4,"li")(5,"a",2),e._uU(6,"Users"),e.qZA()(),e.TgZ(7,"li")(8,"a",3),e._uU(9,"User"),e.qZA()()(),e.TgZ(10,"div",4)(11,"div",5)(12,"div",6)(13,"h1",7),e._uU(14,"User"),e.qZA()(),e.TgZ(15,"div",6),e._UZ(16,"div",8),e.qZA()(),e._UZ(17,"user-tabs"),e.TgZ(18,"div",9),e.YNc(19,_e,4,5,"a",10),e.qZA()()),2&o&&(e.xp6(8),e.MGl("routerLink","/user/user-item/",n.item.id,""),e.xp6(11),e.Q6J("ngForOf",n.supportTeams))},dependencies:[s.sg,s.O5,l.rH,G],encapsulation:2})}return i})();const ge=[{path:"user-list",component:M,data:{activeSideMenu:"Users"}},{path:"user-item/:id",component:H,resolve:{user:g.W.userResolver("id")},canDeactivate:[k.u.isPristine()],data:{activeSideMenu:"Users"}},{path:"user-support-team/:id",component:fe,resolve:{user:g.W.userResolver("id")},canDeactivate:[k.u.isPristine()],data:{activeSideMenu:"Users"}},{path:"user-activity/:id",component:ne,resolve:{user:g.W.userResolver("id")},data:{activeSideMenu:"Users"}},{path:"user-group-list",component:ae,data:{activeSideMenu:"User Groups"}},{path:"user-group-item/:id",component:ue,resolve:{userGroup:p.Q.userGroupResolver("id")},canDeactivate:[k.u.isPristine()],data:{activeSideMenu:"User Groups"}},{path:"user-group-claims/:id",component:le,resolve:{userGroup:p.Q.userGroupResolver("id")},canDeactivate:[k.u.isPristine()],data:{activeSideMenu:"User Groups"}}];let he=(()=>{class i{static#e=this.\u0275fac=function(o){return new(o||i)};static#t=this.\u0275mod=e.oAB({type:i});static#i=this.\u0275inj=e.cJS({imports:[l.Bz.forChild(ge),l.Bz]})}return i})(),ve=(()=>{class i{static#e=this.\u0275fac=function(o){return new(o||i)};static#t=this.\u0275mod=e.oAB({type:i});static#i=this.\u0275inj=e.cJS({providers:[U,F,x],imports:[s.ez,_.m,he]})}return i})()},1986:(q,A,a)=>{a.d(A,{k:()=>p});var s=a(9212),_=a(7147),l=a(6814);function e(c,Z){if(1&c){const f=s.EpF();s.TgZ(0,"a",3),s.NdJ("click",function(){s.CHM(f);const T=s.oxw(2);return s.KtG(T.onAdd())}),s._uU(1,"add"),s.qZA()}}function r(c,Z){if(1&c&&s.YNc(0,e,2,0,"a",2),2&c){const f=s.oxw();s.Q6J("ngIf",f.canAdd)}}function m(c,Z){if(1&c){const f=s.EpF();s.TgZ(0,"a",6),s.NdJ("click",function(){s.CHM(f);const T=s.oxw().$implicit,b=s.oxw();return s.KtG(b.onEdit(T))}),s._uU(1,"view/edit"),s.qZA()}}function d(c,Z){if(1&c){const f=s.EpF();s.TgZ(0,"span"),s._uU(1," \xa0 "),s.TgZ(2,"a",7),s.NdJ("click",function(){s.CHM(f);const T=s.oxw().$implicit,b=s.oxw();return s.KtG(b.onDelete(T))}),s._uU(3,"delete"),s.qZA()()}}function g(c,Z){if(1&c&&s.YNc(0,m,2,0,"a",4)(1,d,4,0,"span",5),2&c){const f=s.oxw();s.Q6J("ngIf",f.canEdit),s.xp6(),s.Q6J("ngIf",f.canDelete)}}let p=(()=>{class c extends _.I{constructor(){super(),this.canAdd=!0,this.add=new s.vpe,this.canEdit=!0,this.edit=new s.vpe,this.canDelete=!0,this.delete=new s.vpe,this.justify="right"}onAdd(){this.add.emit()}onEdit(f){this.edit.emit(f)}onDelete(f){this.delete.emit(f)}static#e=this.\u0275fac=function(U){return new(U||c)};static#t=this.\u0275cmp=s.Xpm({type:c,selectors:[["grid-action-column"]],inputs:{canAdd:"canAdd",canEdit:"canEdit",canDelete:"canDelete"},outputs:{add:"add",edit:"edit",delete:"delete"},features:[s._Bn([{provide:_.I,useExisting:c}]),s.qOj],decls:4,vars:0,consts:[["headTemplate",""],["itemTemplate",""],["class","link is-success",3,"click",4,"ngIf"],[1,"link","is-success",3,"click"],["class","link is-primary",3,"click",4,"ngIf"],[4,"ngIf"],[1,"link","is-primary",3,"click"],[1,"link","is-danger",3,"click"]],template:function(U,T){1&U&&s.YNc(0,r,1,1,"ng-template",null,0,s.W1O)(2,g,2,2,"ng-template",null,1,s.W1O)},dependencies:[l.O5],encapsulation:2})}return c})()},9510:(q,A,a)=>{a.d(A,{K:()=>r});var s=a(7147),_=a(9212),l=a(6814);function e(m,d){if(1&m&&(_._uU(0),_.ALo(1,"date")),2&m){const g=d.$implicit,p=_.oxw();_.hij(" ",_.xi3(1,1,g[p.name],"dd MMM yyyy"),"\n")}}let r=(()=>{class m extends s.I{static#e=this.\u0275fac=(()=>{let g;return function(c){return(g||(g=_.n5z(m)))(c||m)}})();static#t=this.\u0275cmp=_.Xpm({type:m,selectors:[["grid-date-column"]],features:[_._Bn([{provide:s.I,useExisting:m}]),_.qOj],decls:2,vars:0,consts:[["itemTemplate",""]],template:function(p,c){1&p&&_.YNc(0,e,2,4,"ng-template",null,0,_.W1O)},dependencies:[l.uU],encapsulation:2})}return m})()},3773:(q,A,a)=>{a.d(A,{a:()=>m});var s=a(7147),_=a(5619),l=a(9212),e=a(6814);function r(d,g){if(1&d&&(l._uU(0),l.ALo(1,"async")),2&d){const p=g.$implicit,c=l.oxw();l.hij(" ",c.format(l.lcZ(1,1,c.lookup(p[c.name]))),"\n")}}let m=(()=>{class d extends s.I{constructor(){super(...arguments),this._cache={}}lookup(p){if(null==this._cache[p]){if(this._cache[p]=new _.X(null),!this.dataSource)return new _.X(null);this.dataSource.query({skip:0,take:1,filters:[{field:"id",op:"eq",value:p}]}).subscribe(c=>{1==c.items.length&&this._cache[p].next(c.items[0])})}return this._cache[p]}format(p){return p?p.name:""}static#e=this.\u0275fac=(()=>{let p;return function(Z){return(p||(p=l.n5z(d)))(Z||d)}})();static#t=this.\u0275cmp=l.Xpm({type:d,selectors:[["grid-reference-column"]],inputs:{dataSource:"dataSource"},features:[l._Bn([{provide:s.I,useExisting:d}]),l.qOj],decls:2,vars:0,consts:[["itemTemplate",""]],template:function(c,Z){1&c&&l.YNc(0,r,2,3,"ng-template",null,0,l.W1O)},dependencies:[e.Ov],encapsulation:2})}return d})()}}]);