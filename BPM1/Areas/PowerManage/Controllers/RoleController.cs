
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BPM1;
using BPM.Repository.PowerManage;
using BPM.Models;

namespace BPM1.Areas.PowerManage.Controllers
{
    [Area("PowerManage")]
    public class RoleController : Controller
    {

        PowerRoleRepository _roleRepository;
        PowerRoleMenuRepository _roleMenuRepository;
        public RoleController(PowerRoleRepository roleRepository, PowerRoleMenuRepository roleMenuRepository)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ������༭
        /// </summary>
        /// <returns></returns>
        public IActionResult RoleAddOrEdit()
        {
            return View();
        }

        /// <summary>
        /// ѡ����Ա
        /// </summary>
        /// <returns></returns>
        public IActionResult ChoosePersons()
        {
            return View();
        }

        /// <summary>
        /// ѡ���ɫ��Ӧ�˵�
        /// </summary>
        /// <returns></returns>
        public IActionResult ChooseMenus()
        {
            return View();
        }
        public IActionResult showMenus()
        {
            return View();
        }

        ///// <summary>
        ///// ����RoleId��ȡȨ��
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //public string GetPowerMenuJson(string roleId)
        //{
        //    List<MiniTreeNode2> tree = new List<MiniTreeNode2>();
        //    if (!string.IsNullOrEmpty(roleId))
        //    {

        //        #region �������
        //        List<ParameterJson> list = new List<ParameterJson>();
        //        list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //        list.Add(new ParameterJson("IsEnable", ConditionOperate.Equal.ToString(), "1"));
        //        list.Add(new ParameterJson("IsSuperUser", ConditionOperate.Equal.ToString(), "0"));
        //        string sCondition = JsonConvert.SerializeObject(list);
        //        #endregion

        //        string strSort = new JqSortParam().ToJson();

        //        //string sJson = _menubll.GetListJson(sCondition, strSort);
        //        string sJson = _menubll.GetMenusByRoleId(roleId, ManageProvider.Current.U_AreaCode);
        //        if (!string.IsNullOrEmpty(sJson))
        //        {
        //            List<Power_MenuView> roleMenuList1 = JsonConvert.DeserializeObject<List<Power_MenuView>>(sJson);
        //            var o = roleMenuList1.Where(x => x.IsShow == true).OrderBy(m => m.U_SortNo).ToList();
        //            foreach (var item in o)
        //            {
        //                MiniTreeNode2 node = new MiniTreeNode2();
        //                node.id = item.ID.ToString();
        //                node.pid = item.ParentID.ToString();
        //                node.text = item.Name;
        //                //node.@checked = roleMenuList.Where(x => x.MenuID == item.ID).Count() > 0 ? true : false;
        //                tree.Add(node);
        //            }
        //        }
        //    }
        //    return JsonConvert.SerializeObject(tree);
        //}


        /// <summary>
        /// ������༭
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(Power_Role model)
        {
            //Power_RoleView model = new Power_RoleView();
            //List<ParameterJson> list = new List<ParameterJson>();
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            ////list.Add(new ParameterJson("U_IsSystem", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            //string json = _workbenchBll.GetListJson(sCondition);
            //List<Power_WorkbenchView> workbenchList = JsonConvert.DeserializeObject<List<Power_WorkbenchView>>(json);
            //var a = workbenchList.Where(x => x.Name == "Ĭ�Ͻ�ɫ����̨").FirstOrDefault();
            //�ж�ǰ̨��������model��id�Ƿ�Ϊ��
            if (model.ID == Guid.Empty)
            {
                model.U_IsSystem = false;
                model.U_IsValid = true;
                model.U_AreaCode = ManageProvider.AreaCode;

                try
                {
                    _roleRepository.Add(model);
                    //��¼��־
                    //logcache.AddServiceLogForAdd<Power_RoleView>(model, CurrentMenuID, "������ɫ�ɹ�", "Power_Role");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForAdd<Power_RoleView>(model, CurrentMenuID, "������ɫ�쳣", "Power_Role", Request.Path, ex.Message, ex.StackTrace);
                }
            }
            else
            {
                Power_Role model1 = _roleRepository.GetById(model.ID);
                //model = _rolebll.GetViewModel(data.ID.ToString());
             
                try
                {
                    model1.Name = model.Name;
                    model1.Status = model.Status;
                    model1.WorkbenchID = model.WorkbenchID;
                    model1.Remarks = model.Remarks;
                    model1.U_LastModifiedDate = DateTime.Now;
                    model1.U_IsSystem = false;

                    _roleRepository.Edit(model1);
                    //��¼��־
                    //logcache.AddServiceLogForUpdate(model, oldModel, CurrentMenuID, "���½�ɫ�ɹ�", "Power_Role");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForUpdate(model, oldModel, CurrentMenuID, "���½�ɫ�쳣", "Power_Role", Request.Path, ex.Message, ex.StackTrace);
                }
            }
        }

        ///// <summary>
        ///// ��ɫѡ��ȷ����ť
        ///// </summary>
        ///// <param name="data"></param>
        //public void UserRoleSave(Power_UserRoleView data)
        //{
        //    Power_UserRoleView model = new Power_UserRoleView();
        //    Power_UserRoleView modelNew = new Power_UserRoleView();
        //    try
        //    {

        //        model = _userroleBll.GetViewModel(data.ID.ToString());
        //        //��ɫID;
        //        model.RoleID = data.RoleID;
        //        modelNew = _userroleBll.Update(model);
        //        logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "���½�ɫѡ��", "Power_UserRole");
        //    }
        //    catch (Exception ex)
        //    {
        //        logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "���½�ɫѡ���쳣", "Power_UserRole");
        //    }


        //}

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetEntity(string id)
        {
            Power_Role model = _roleRepository.GetById(Guid.Parse(id));
            //Power_RoleView model = _rolebll.GetViewModel(id);
            return JsonConvert.SerializeObject(model);
        }

        ///// <summary>
        ///// ��ȡ��ɫ��Ա��ϵ����
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public string GetUserRoleEntity(string id)
        //{
        //    Power_UserRoleView model = _userroleBll.GetViewModel(id);
        //    return JsonConvert.SerializeObject(model);
        //}

        ///// <summary>
        ///// ��ȡjson��������̨����
        ///// </summary>
        ///// <returns></returns>
        //public string GetJsonListForRoles(string roleName)
        //{
        //    #region �������
        //    List<ParameterJson> list = new List<ParameterJson>();
        //    list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //    list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
        //    //list.Add(new ParameterJson("U_IsSystem", ConditionOperate.Equal.ToString(), "1"));
        //    if (!string.IsNullOrEmpty(roleName))
        //    {
        //        list.Add(new ParameterJson("Name", ConditionOperate.Like.ToString(), roleName));
        //    }
        //    string sCondition = JsonConvert.SerializeObject(list);
        //    #endregion

        //    #region ����
        //    string strSort = new JqSortParam().ToJson();
        //    #endregion
        //    string strJson = _rolebll.GetListJson(sCondition, strSort);
        //    return strJson;
        //}

        ///// <summary>
        ///// ���ݽ�ɫ��ȡ�˵�ID�б�
        ///// </summary>
        ///// <param name="roleId">��ɫID</param>
        ///// <returns></returns>
        //public string GetRoleMenuJson(string roleId)
        //{
        //    List<ParameterJson> list1 = new List<ParameterJson>();
        //    list1.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //    list1.Add(new ParameterJson("RoleId", ConditionOperate.Equal.ToString(), roleId));
        //    string json = _rolemenubll.GetListJson(JsonConvert.SerializeObject(list1));

        //    return json;
        //}


        /// <summary>
        /// �б��ҳ
        /// </summary>
        /// <param name="ParameterJson"></param>
        /// <param name="jqgridparam"></param>
        /// <returns></returns>
        public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
        {
            if (string.IsNullOrEmpty(jqgridparam.sortField))
                jqgridparam.sortField = "U_SortNo";

            //List<ParameterJson> list = new List<ParameterJson>();
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            //string sCondition = JqConditionParam.CombinationCon(JsonConvert.SerializeObject(list), ParameterJson);

            string areaCode = ManageProvider.AreaCode;
            int records = 0;
            IEnumerable<Power_Role> list = _roleRepository.GetPageList(x => x.U_IsValid == true && x.U_AreaCode == areaCode, jqgridparam.pageSize, jqgridparam.pageIndex, out records);
            jqgridparam.records = records;
            return JsonManager.ToPageJson(jqgridparam, JsonConvert.SerializeObject(list));
        }

        ///// <summary>
        ///// ��ȡ��ɫ�б� ��ɫ��Ա��ϵҳ��ʹ��
        ///// </summary>
        ///// <returns></returns>
        //public string GetRoleJson()
        //{
        //    string sCondition = JqConditionParam.ToJson();
        //    string strSort = new JqSortParam().ToJson();
        //    string strJson = _rolebll.GetListJson(sCondition, strSort);
        //    return strJson;
        //}

        ///// <summary>
        ///// ɾ����ɫ
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool Delete(string id)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        Power_RoleView model = _rolebll.GetViewModel(id);
        //        try
        //        {
        //            _rolebll.Delete(Guid.Parse(id));
        //            //��¼��־
        //            logcache.AddServiceLogForDelete<Power_RoleView>(model, CurrentMenuID, "��ɫɾ��", "");
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            logcache.AddServiceLogForDelete<Power_RoleView>(model, CurrentMenuID, "��ɫɾ���쳣", "", Request.Path, ex.Message, ex.StackTrace);
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// ɾ����ɫ��Ա��Ӧ��ϵ
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool UserRoleDelete(string id)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        Power_UserRoleView model = _userroleBll.GetViewModel(id);
        //        try
        //        {
        //            _userroleBll.Delete(Guid.Parse(id));
        //            logcache.AddServiceLogForDelete<Power_UserRoleView>(model, CurrentMenuID, "ɾ����ɫ��Ա��Ӧ��ϵ", "Power_UserRole");
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            logcache.AddServiceLogForDelete<Power_UserRoleView>(model, CurrentMenuID, "ɾ����ɫ��Ա��Ӧ��ϵ�쳣", "Power_UserRole", Request.Path, ex.Message, ex.StackTrace);
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        /// ��ɫѡ��˵�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menus"></param>
        public void ChooseMenu(string roleid, string menus)
        {
            MessageUser mu = ManageProvider.Current;
            try
            {
                //���жϴ��ݹ����Ĳ˵�id�����Ƿ�Ϊ�գ�Ϊ�ղ��󶨸ý�ɫ�Ĳ˵�
                if (menus == null || string.IsNullOrEmpty(menus))
                {
                    return;
                }

            
                //List<ParameterJson> list = new List<ParameterJson>();
                //list.Add(new ParameterJson("RoleID", ConditionOperate.Equal.ToString(), roleid));
                //string sCondition = JsonConvert.SerializeObject(list);

                //_rolemenubll.Delete(sCondition);

               IEnumerable<Power_RoleMenu> list =  _roleMenuRepository.List(x => x.RoleID == Guid.Parse(roleid));
                foreach (var item in list)
                {
                    _roleMenuRepository.Query().Power_RoleMenu.Remove(item);
                }
                _roleMenuRepository.Query().SaveChanges();





                //List<Power_RoleMenuView> roleMenulist = new List<Power_RoleMenuView>();
                String[] menuArray = menus.Split(',');

                for (int i = 0; i < menuArray.Length; i++)
                {
                    if (CommonHelper.CheckGuid(menuArray[i].ToString()))
                    {
                        //��������һ������
                        Power_RoleMenu model = new Power_RoleMenu();
                        model.RoleID = Guid.Parse(roleid);
                        model.MenuID = Guid.Parse(menuArray[i].ToString());
                        _roleMenuRepository.Query().Power_RoleMenu.Add(model);
                        //roleMenulist.Add(model);

                        //logcache.AddServiceLogForAdd<Power_RoleMenuView>(model, CurrentMenuID, "��ɫѡ��˵����ӳɹ�", "Power_RoleMenu");
                    }
                }
                _roleMenuRepository.Query().SaveChanges();

                //if (roleMenulist.Count > 0)
                //    _rolemenubll.PlentyInsert(roleMenulist);


                #region ���½�ɫ��Ӧ��Ա�˵�                
                //List<ParameterJson> userRolelist = new List<ParameterJson>();
                //list.Add(new ParameterJson("roleid", ConditionOperate.Equal.ToString(), roleid));
                //string sExists = _userroleBll.GetListJson(JsonConvert.SerializeObject(list));
                //List<Power_UserRoleView> existList = JsonConvert.DeserializeObject<List<Power_UserRoleView>>(sExists);


                //List<Guid> userIdList = new List<Guid>();
                //foreach (Power_UserRoleView info in existList)
                //{
                //    if (!userIdList.Exists(d => d == info.UserID.Value))
                //    {
                //        userIdList.Add(info.UserID.Value);
                //    }
                //}
                //_myMenuBll.UpdateUsersMenu(userIdList);
                #endregion
            }
            catch (Exception ex)
            {
                //logcache.AddServiceLogForAdd<Power_RoleMenuView>(new Power_RoleMenuView(), CurrentMenuID, "��ɫѡ��˵������쳣", "Power_RoleMenu", HttpContext.Request.Path, ex.Message, ex.StackTrace);
            }
            //roleCache.AddOrUpdate(roleid, menus, mu.U_AreaCode);
        }

        ///// <summary>
        ///// ��ɫѡ����Ա
        ///// </summary>
        ///// <param name="roleid"></param>
        ///// <param name="persons"></param>
        //public void ChoosePerson(String roleid, String areaCode, String persons)
        //{
        //    Power_UserRoleView modelNew = new Power_UserRoleView();
        //    try
        //    {
        //        //���жϴ��ݹ����Ĳ˵�id�����Ƿ�Ϊ�գ�Ϊ�ղ��󶨸ý�ɫ�Ĳ˵�
        //        if (persons == null || string.IsNullOrEmpty(persons))
        //        {
        //            _userroleBll.DelUserRole(roleid, areaCode);
        //            return;
        //        }
        //        String[] personsArray = persons.Split(',');
        //        //����ʱ��ɾ���ý�ɫ֮ǰ�󶨵����ݣ��ٲ���������
        //        _userroleBll.DelUserRole(roleid, areaCode);
        //        for (int i = 0; i < personsArray.Length; i++)
        //        {
        //            //�ж�ȡ�������û�id�Ƿ���Guid
        //            if (CommonHelper.CheckGuid(personsArray[i].ToString()))
        //            {
        //                Power_UserRoleView model = new Power_UserRoleView();
        //                model.RoleID = Guid.Parse(roleid);
        //                model.UserID = Guid.Parse(personsArray[i].ToString());
        //                modelNew = _userroleBll.Insert(model);
        //                logcache.AddServiceLogForAdd(model, CurrentMenuID, "��ɫѡ����Ա���ӳɹ�", " Power_UserRole");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logcache.AddServiceLogForAdd(modelNew, CurrentMenuID, "��ɫѡ����Ա�����쳣", " Power_UserRole", HttpContext.Request.Path, ex.Message, ex.StackTrace);
        //    }




        //}

    }
}