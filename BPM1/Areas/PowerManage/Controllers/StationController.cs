using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BPM1;
using BPM.Repository.PowerManage;
using BPM.Models;

namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
{
    [Area("PowerManage")]
    public class StationController : Controller
    {
        PowerStationRepository _stationRepository;
        public StationController(PowerStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }


        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //logcache.AddServiceLogForView(CurrentMenuID, "������λ�������", "");
            return View();
        }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <returns></returns>
        public IActionResult AddOrEditStationView(String id)
        {
            return View();
        }

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

            jqgridparam.pageIndex = 1;
            jqgridparam.pageSize = 20;
            jqgridparam.records = 1;
            int records = 0;

            IEnumerable<Power_Stations> list = _stationRepository.GetPageList(x => x.U_AreaCode == "", jqgridparam.pageIndex, jqgridparam.pageSize, out records);
            jqgridparam.records = records;
            //string strJson = _stationbll.FindListJsonPage(ParameterJson, ref jqgridparam);
            return JsonManager.ToPageJson(jqgridparam, JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// ��ȡViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetEntity(string id)
        {
            var model = _stationRepository.GetById(Guid.Parse(id));
            return JsonConvert.SerializeObject(model);
            //Power_StationsView entity = _stationbll.GetViewModel(id);
            //return entity.ToJson();
        }

        ///// <summary>
        ///// ��ȡ��λ��json
        ///// </summary>
        ///// <returns></returns>
        //public string GetStationJson()
        //{

        //    #region �������
        //    string sCondition = JqConditionParam.ToJson();
        //    #endregion

        //    #region ����
        //    string strSort = new JqSortParam().ToJson();

        //    #endregion

        //    string strJson = _stationbll.GetListJson(sCondition, strSort);
        //    return strJson;
        //}

        /// <summary>
        /// �༭������
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(Power_Stations model)
        {
            Power_Stations data = new Power_Stations();
            //�ж�ǰ�˷��ػ�����model��id�Ƿ�Ϊ��
            if (model.ID == Guid.Empty)
            {
                data = model;
                data.U_CreateDate = DateTime.Now;
                data.ID = Guid.NewGuid();

                try
                {
                    _stationRepository.Add(data);
                    //_stationbll.Insert(data);
                    //logcache.AddServiceLogForAdd<Power_StationsView>(data, CurrentMenuID, "������λ�ɹ�", "Power_Stations");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForAdd<Power_StationsView>(data, CurrentMenuID, "������λ�쳣", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
                }
            }
            else
            {
                data = _stationRepository.GetById(model.ID);
                //data = _stationbll.GetViewModel(model.ID.ToString());
                //Power_StationsView oldData = ObjectClone.ToClone<Power_StationsView>(data);

                try
                {
                    data.Code = model.Code;
                    data.Name = model.Name;
                    data.Remarks = model.Remarks;
                    data.U_LastModifiedDate = DateTime.Now;
                    _stationRepository.Edit(data);
                    //_stationbll.Update(data);
                    //logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "���¸�λ�ɹ�", "Power_Stations");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "���¸�λ�쳣", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Power_Stations model = _stationRepository.GetById(Guid.Parse(id));
                //Power_StationsView model = _stationbll.GetViewModel(id);
                try
                {
                    _stationRepository.Delete(model);
                    //_stationbll.Delete(Guid.Parse(id));
                    //logcache.AddServiceLogForDelete<Power_StationsView>(model, CurrentMenuID, "ɾ����λ�ɹ�", "Power_Stations");
                    return true;
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForDelete<Power_StationsView>(model, CurrentMenuID, "ɾ����λ�쳣", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
                    return false;
                }

            }
            return false;
        }

        /// <summary>
        /// ����ʱ��֤�����ֵ�Ƿ�Ψһ
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="filed">�ֶ���</param>
        /// <param name="val">�ֶ�ֵ</param>
        /// <returns></returns>
        public bool FindOnly(string stationCode)
        {
            #region �������
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), stationCode));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion
            int num = _stationRepository.List(x => x.Code == stationCode && x.U_AreaCode == ManageProvider.Current.U_AreaCode && x.U_IsValid == true).Count();
            return num == 0;
        }

        /// <summary>
        /// �༭ʱ��֤�����ֵ�Ƿ�Ψһ
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="filed">�ֶ���</param>
        /// <param name="val">�ֶ�ֵ</param>
        /// <returns></returns>
        public bool FindOnlyEdit(string stationCode, string ID)
        {
            #region �������
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), stationCode));
            //list.Add(new ParameterJson("ID", ConditionOperate.NotEqual.ToString(), ID));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion
            int num = _stationRepository.List(x => x.Code == stationCode && x.ID != Guid.Parse(ID) && x.U_AreaCode == ManageProvider.Current.U_AreaCode && x.U_IsValid == true).Count();
            return num == 0;
        }
    }
}