namespace JDGame.Framework
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using Google.Protobuf;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using GameFramework.DataTable;
    using System;
    using System.Text.RegularExpressions;

    public class UIMainPanelCtrl : UGuiForm
    {
        private UIMainPanelView _View;

        /// <summary>
        /// 房间列表数据
        /// </summary>
        private List<RoomListsItemData> _RoomListsItemDataLists;
        /// <summary>
        /// 俱乐部列表数据
        /// </summary>
        private List<ClubListItemData> _ClubListItemDataLists;
        /// <summary>
        /// 俱乐部成员数据
        /// </summary>
        private List<MemberInfo> _ClubPeopleItemDataLists;
        /// <summary>
        /// 申请加入俱乐部成员列表
        /// </summary>
        private List<ApplyMemberInfo> _ApplyClubPeopleItemDataLists;
        /// <summary>
        /// 商店数据
        /// </summary>
        private List<GoodsData> _ShopGoodsItemDataLists;
        private List<HistorysData> _HistoryItemDataLists;

        private List<NotesItemData> _NotesItemDataLists;

        private List<GetMyRoomListsData> _GetMyRoomItemDataLists;
        private List<GamePlayersData> _GetMyRoomTableItemDataLists;
        /// <summary>
        /// 基金发放日志数据
        /// </summary>
        private List<GetFundListsData> _GetFundListsData;

        private List<CtrlItemData> _ClubCtrlItemListsData;
        private List<ClubHistroyItemData> _ClubHistroyItemDataLists;
        private List<WalletLogsItemData> _WalletLogsItemDataLists;

        /// <summary>
        /// 小盲
        /// </summary>
        private int _CurrSmallBlind;
        /// <summary>
        /// 大盲
        /// </summary>
        private int _CurrBigBlind;
        /// <summary>
        /// 最小带入筹码
        /// </summary>
        private int _CurrMinCarry;
        /// <summary>
        /// 最大带入筹码
        /// </summary>
        private int _CurrMaxCarry;
        /// <summary>
        /// 当前牌局人数
        /// </summary>
        private int _CurrPeopleNumber;
        /// <summary>
        /// 当前牌局时长
        /// </summary>
        private int _CurrPokerTime;
        /// <summary>
        /// ante
        /// </summary>
        private int _CurrAnteNumber;
        /// <summary>
        /// 总带入
        /// </summary>
        private int _CurrTotalCarryNumber;

        /// <summary>
        /// 当前操作
        /// </summary>
        private ClubOpType _CurrClubOpType;

        /// <summary>
        /// 当前选中的俱乐部数据
        /// </summary>
        private ClubListItemData _CurrChooseClubListsItemData = new ClubListItemData();

        /// <summary>
        /// 当前执行的俱乐部操作---公共操作
        /// </summary>
        private PublicOpType _CurrPublicOpType;
        /// <summary>
        /// 当前要删除的俱乐部成员Id
        /// </summary>
        private int _CurrDeleteMemberUserId;

        /// <summary>
        /// 是否是第一次打开这个界面
        /// </summary>
        private bool _IsFirstOpen;

        /// <summary>
        /// 当前接收到的公告内容
        /// </summary>
        private string _CurrNotes;

        /// <summary>
        /// 是否修改过地区
        /// </summary>
        private bool _IsChangeArea;

        /// <summary>
        /// 是否创建战队局
        /// </summary>
        private bool _IsCreatePokerTeam;
        /// <summary>
        /// 头像大小
        /// </summary>
        private Vector2 _IconSize;


        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _View = this.gameObject.Get<UIMainPanelView>();
            _RoomListsItemDataLists = new List<RoomListsItemData>();
            _ClubListItemDataLists = new List<ClubListItemData>();
            _ClubPeopleItemDataLists = new List<MemberInfo>();
            _ApplyClubPeopleItemDataLists = new List<ApplyMemberInfo>();
            _ShopGoodsItemDataLists = new List<GoodsData>();
            _HistoryItemDataLists = new List<HistorysData>();
            _NotesItemDataLists = new List<NotesItemData>();
            _GetMyRoomItemDataLists = new List<GetMyRoomListsData>();
            _GetMyRoomTableItemDataLists = new List<GamePlayersData>();
            _GetFundListsData = new List<GetFundListsData>();
            _ClubCtrlItemListsData = new List<CtrlItemData>();
            _ClubHistroyItemDataLists = new List<ClubHistroyItemData>();
            _WalletLogsItemDataLists = new List<WalletLogsItemData>();

            _CurrNotes = "";
            _IsFirstOpen = true;

            _View.GongGaoButton.Get<UIButton>().AddButtonClickFunction(_View.GongGaoButton, OnGongGaoButtonClicked);
            _View.YueJuButton.Get<UIButton>().AddButtonClickFunction(_View.YueJuButton, OnYueJuButtonClicked);
            _View.FaXianButton.Get<UIButton>().AddButtonClickFunction(_View.FaXianButton, OnFaXianButtonClicked);
            _View.JuLeBuButton.Get<UIButton>().AddButtonClickFunction(_View.JuLeBuButton, OnJuLeBuButtonClciked);
            _View.MeButton.Get<UIButton>().AddButtonClickFunction(_View.MeButton, OnMeButtonClciked);

            _View.YJ_JoinMatchButton.Get<UIButton>().AddButtonClickFunction(_View.YJ_JoinMatchButton, OnYJ_JoinMatchButtonClicked);
            _View.YJ_CreateMatchButton.Get<UIButton>().AddButtonClickFunction(_View.YJ_CreateMatchButton, OnYJ_CreateMatchButtonClicked);
            _View.CM_HightSettingButton.Get<UIButton>().AddButtonClickFunction(_View.CM_HightSettingButton, OnCM_HightSettingButton);
            _View.CM_OkButton.Get<UIButton>().AddButtonClickFunction(_View.CM_OkButton, OnCM_OkButton);
            _View.CM_BackButton.Get<UIButton>().AddButtonClickFunction(_View.CM_BackButton, OnCM_BackButton);
            _View.CM_HightSettingOkButton.Get<UIButton>().AddButtonClickFunction(_View.CM_HightSettingOkButton, OnCM_HightSettingOkButton);
            _View.CM_HightSettingHelpButton.Get<UIButton>().AddButtonClickFunction(_View.CM_HightSettingHelpButton, OnCM_HightSettingHelpButton);
            _View.CM_HightSettingTipsBackButton.Get<UIButton>().AddButtonClickFunction(_View.CM_HightSettingTipsBackButton, OnCM_HightSettingTipsBackButton);
            _View.CM_HightSettingBackButton.Get<UIButton>().AddButtonClickFunction(_View.CM_HightSettingBackButton, OnCM_HightSettingBackButton);

            _View.CB_JoinClubButton.Get<UIButton>().AddButtonClickFunction(_View.CB_JoinClubButton, OnJLB_JoinClubButtonClciked);
            _View.CB_CreateClubButton.Get<UIButton>().AddButtonClickFunction(_View.CB_CreateClubButton, OnJLB_CreateClubButtonClciked);
            _View.CB_Small_oinClubButton.Get<UIButton>().AddButtonClickFunction(_View.CB_Small_oinClubButton, OnJLB_JoinClubButtonClciked);
            _View.CB_Small_CreateClubButton.Get<UIButton>().AddButtonClickFunction(_View.CB_Small_CreateClubButton, OnJLB_CreateClubButtonClciked);

            //            _View.FX_SmallButton.Get<UIButton>().AddButtonClickFunction(_View.FX_SmallButton, OnFX_SmallButton);
            //            _View.FX_MaxButton.Get<UIButton>().AddButtonClickFunction(_View.FX_MaxButton, OnFX_MaxButton);
            _View.CB_CreateClubOkButton.Get<UIButton>().AddButtonClickFunction(_View.CB_CreateClubOkButton, OnCB_CreateClubOkButton);
            _View.CB_CreateClubBackButton.Get<UIButton>().AddButtonClickFunction(_View.CB_CreateClubBackButton, OnCB_CreateClubBackButton);
            _View.CB_AddClubCloseButton.Get<UIButton>().AddButtonClickFunction(_View.CB_AddClubCloseButton, OnCB_AddClubCloseButton);
            _View.CB_CreateListsAddClubButton.Get<UIButton>().AddButtonClickFunction(_View.CB_CreateListsAddClubButton, OnCB_CreateListsAddClubButton);
            _View.CB_DCCBackButton.Get<UIButton>().AddButtonClickFunction(_View.CB_DCCBackButton, OnCB_DCCBackButton);
            _View.CB_DCCBuyVIPButton.Get<UIButton>().AddButtonClickFunction(_View.CB_DCCBuyVIPButton, OnCB_DCCBuyVIPButton);

            _View.CB_JoinClubBackButton.Get<UIButton>().AddButtonClickFunction(_View.CB_JoinClubBackButton, OnCB_JoinClubBackButton);
            _View.CB_JoinClubOkButton.Get<UIButton>().AddButtonClickFunction(_View.CB_JoinClubOkButton, OnCB_JoinClubOkButton);

            _View.Me_ShopButton.Get<UIButton>().AddButtonClickFunction(_View.Me_ShopButton, OnMe_ShopButton);
            _View.Me_HistoryButton.Get<UIButton>().AddButtonClickFunction(_View.Me_HistoryButton, OnMe_HistoryButton);
            _View.Me_SettingButton.Get<UIButton>().AddButtonClickFunction(_View.Me_SettingButton, OnMe_SettingButton);
            _View.Me_SummaryButton.Get<UIButton>().AddButtonClickFunction(_View.Me_SummaryButton, OnMe_SummaryButton);
            _View.AnalysisButton.Get<UIButton>().AddButtonClickFunction(_View.AnalysisButton, OnAnalysisButton);
            _View.SpecialButton.Get<UIButton>().AddButtonClickFunction(_View.SpecialButton, OnSpecialButton);
            _View.BuyLevelCardButton.Get<UIButton>().AddButtonClickFunction(_View.BuyLevelCardButton, OnBuyLevelCardButton);
            _View.S_BackButton.Get<UIButton>().AddButtonClickFunction(_View.S_BackButton, OnS_BackButton);

            _View.Me_HeadImage.gameObject.Get<UIButton>().AddButtonClickFunction(_View.Me_HeadImage.gameObject, OnHeadImageButton);
            _View.CameraButton.Get<UIButton>().AddButtonClickFunction(_View.CameraButton, OnCameraButton);
            _View.PhotoButton.Get<UIButton>().AddButtonClickFunction(_View.CameraButton, OnPhotoButton);
            _View.CloseChooseTypeDialogButton.Get<UIButton>().AddButtonClickFunction(_View.CloseChooseTypeDialogButton, OnCloseChooseTypeDialogButton);
            _View.CloseChooseTypeDialogButton02.Get<UIButton>().AddButtonClickFunction(_View.CloseChooseTypeDialogButton02, OnCloseChooseTypeDialogButton02);
            _View.MyClubHeadImageButton.gameObject.Get<UIButton>().AddButtonClickFunction(_View.MyClubHeadImageButton.gameObject, OnMyClubHeadImageButton);

            _View.SettingBackButton.Get<UIButton>().AddButtonClickFunction(_View.SettingBackButton, OnSettingBackButton);
            _View.ShopGiftBackButton.Get<UIButton>().AddButtonClickFunction(_View.ShopGiftBackButton, OnShopGiftBackButton);
            _View.ChargeBackButton.Get<UIButton>().AddButtonClickFunction(_View.ChargeBackButton, OnChargeBackButton);
            _View.HistoryBackButton.Get<UIButton>().AddButtonClickFunction(_View.HistoryBackButton, OnHistoryBackButton);
            _View.ShopGiftToChargeButton.Get<UIButton>().AddButtonClickFunction(_View.ShopGiftToChargeButton, OnShopGiftToChargeButton);
            _View.LogoutButton.Get<UIButton>().AddButtonClickFunction(_View.LogoutButton, OnLogoutButton);

            _View.Other_JoinButton.Get<UIButton>().AddButtonClickFunction(_View.Other_JoinButton, OnOther_JoinButton);
            _View.Other_BackButton.Get<UIButton>().AddButtonClickFunction(_View.Other_BackButton, OnOther_BackButton);

            _View.My_BackButton.Get<UIButton>().AddButtonClickFunction(_View.My_BackButton, OnMy_BackButton);
            _View.My_SaveButton.Get<UIButton>().AddButtonClickFunction(_View.My_SaveButton, OnOMy_SaveButton);
            _View.My_DissolveButton.Get<UIButton>().AddButtonClickFunction(_View.My_DissolveButton, OnMy_DissolveButton);
            _View.EditorClub_AreaButton.Get<UIButton>().AddButtonClickFunction(_View.EditorClub_AreaButton, OnEditorClub_AreaButton);
            _View.EditorClub_ExitButton.Get<UIButton>().AddButtonClickFunction(_View.EditorClub_ExitButton, OnEditorClub_ExitButton);
            _View.MyClubListsBackButton.Get<UIButton>().AddButtonClickFunction(_View.MyClubListsBackButton, OnMyClubListsBackButton);

            _View.MyClubPeopleButton.Get<UIButton>().AddButtonClickFunction(_View.MyClubPeopleButton, OnMyClubPeopleButton);
            _View.MyClubPeopleListsBackButton.Get<UIButton>().AddButtonClickFunction(_View.MyClubPeopleListsBackButton, OnMyClubPeopleListsBackButton);
            //            _View.My_GetApplyClubPeopleButton.Get<UIButton>().AddButtonClickFunction(_View.My_GetApplyClubPeopleButton, OnMy_GetApplyClubPeopleButton);

            _View.PublicOkButton.Get<UIButton>().AddButtonClickFunction(_View.PublicOkButton, OnPublicOkButton);
            _View.PublicCancelButton.Get<UIButton>().AddButtonClickFunction(_View.PublicCancelButton, OnPublicCancelButton);
            _View.ApplyClubPeopleListsBackButton.Get<UIButton>().AddButtonClickFunction(_View.ApplyClubPeopleListsBackButton, OnApplyClubPeopleListsBackButton);

            _View.ChooseAreaButton.Get<UIButton>().AddButtonClickFunction(_View.ChooseAreaButton, OnChooseAreaButton);

            _View.RuchiLvTipsButton.Get<UIButton>().AddButtonDownFunction(_View.RuchiLvTipsButton, OnRuchiLvTipsButtonDown);
            _View.RuchiLvTipsButton.Get<UIButton>().AddButtonUpFunction(_View.RuchiLvTipsButton, OnRuchiLvTipsButtonUp);
            _View.RuchiShengLvTipsButton.Get<UIButton>().AddButtonDownFunction(_View.RuchiShengLvTipsButton, OnRuchiShengLvTipsButtonDown);
            _View.RuchiShengLvTipsButton.Get<UIButton>().AddButtonUpFunction(_View.RuchiShengLvTipsButton, OnRuchiLvTipsButtonUp);
            _View.PFRTipsButton.Get<UIButton>().AddButtonDownFunction(_View.PFRTipsButton, OnPFRTipsButtonDown);
            _View.PFRTipsButton.Get<UIButton>().AddButtonUpFunction(_View.PFRTipsButton, OnRuchiLvTipsButtonUp);
            _View.BetTipsButton.Get<UIButton>().AddButtonDownFunction(_View.BetTipsButton, OnBetTipsButtonDown);
            _View.BetTipsButton.Get<UIButton>().AddButtonUpFunction(_View.BetTipsButton, OnRuchiLvTipsButtonUp);
            _View.AFTipsButton.Get<UIButton>().AddButtonDownFunction(_View.AFTipsButton, OnAFTipsButtonDown);
            _View.AFTipsButton.Get<UIButton>().AddButtonUpFunction(_View.AFTipsButton, OnRuchiLvTipsButtonUp);
            _View.CBetTipsButton.Get<UIButton>().AddButtonDownFunction(_View.CBetTipsButton, OnCBetTipsButtonDown);
            _View.CBetTipsButton.Get<UIButton>().AddButtonUpFunction(_View.CBetTipsButton, OnRuchiLvTipsButtonUp);

            _View.AnalysisBackButton.Get<UIButton>().AddButtonClickFunction(_View.AnalysisBackButton, OnAnalysisBackButton);

            _View.SevenHistoryButton.Get<UIButton>().AddButtonClickFunction(_View.SevenHistoryButton, OnMe_HistoryButton);
            _View.AllHistoryButton.Get<UIButton>().AddButtonClickFunction(_View.AllHistoryButton, OnAllHistoryButton);
            _View.Me_MessageButton.Get<UIButton>().AddButtonClickFunction(_View.Me_MessageButton, OnMe_MessageButton);

            _View.AboundMeButton.Get<UIButton>().AddButtonClickFunction(_View.AboundMeButton, OnAboundMeButton);
            _View.ChangePasswordButton.Get<UIButton>().AddButtonClickFunction(_View.ChangePasswordButton, OnChangePasswordButton);
            _View.CleanButton.Get<UIButton>().AddButtonClickFunction(_View.CleanButton, OnCleanButton);

            _View.SoundButton.Get<UIButton>().AddButtonClickFunction(_View.SoundButton, OnSoundButton);
            _View.VibrationButton.Get<UIButton>().AddButtonClickFunction(_View.VibrationButton, OnVibrationButton);

            _View.ForgotOkButton.Get<UIButton>().AddButtonClickFunction(_View.ForgotOkButton, OnForgotOkButton);
            _View.ForgotBackButton.Get<UIButton>().AddButtonClickFunction(_View.ForgotBackButton, OnForgotBackButton);
            _View.ForgotGetCodeButton.Get<UIButton>().AddButtonClickFunction(_View.ForgotGetCodeButton, OnForgotGetCodeButton);

            _View.AbountWeBackButton.Get<UIButton>().AddButtonClickFunction(_View.AbountWeBackButton, OnAbountWeBackButton);
            _View.NotesBackButton.Get<UIButton>().AddButtonClickFunction(_View.NotesBackButton, OnNotesBackButton);
            for (int i = 0; i < 5; i++)
            {
                _View.BuyDiamondButtons[i].Get<UIButton>().AddButtonClickFunction(_View.BuyDiamondButtons[i], OnBuyDiamondButtons);
            }

            _View.Me_MyGameRoomButton.Get<UIButton>().AddButtonClickFunction(_View.Me_MyGameRoomButton, OnMe_MyGameRoomButton);
            _View.GetMyRoomDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.GetMyRoomDialogBackButton, OnGetMyRoomDialogBackButton);
            _View.GetMyRoomTableDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.GetMyRoomTableDialogBackButton, OnGetMyRoomTableDialogBackButton);
            _View.FundListsButton.Get<UIButton>().AddButtonClickFunction(_View.FundListsButton, OnFundListsButton);
            _View.MyClubFundListsDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.MyClubFundListsDialogBackButton, OnMyClubFundListsDialogBackButton);

            _View.GiveFundDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.GiveFundDialogBackButton, OnGiveFundDialogBackButton);
            _View.GiveFundOkButton.Get<UIButton>().AddButtonClickFunction(_View.GiveFundOkButton, OnGiveFundOkButton);
            _View.GetMyRoomTableBaoXianMoreButton.Get<UIButton>().AddButtonClickFunction(_View.GetMyRoomTableBaoXianMoreButton, OnGetMyRoomTableBaoXianMoreButton);
            _View.BaoXianMoreDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.BaoXianMoreDialogBackButton, OnBaoXianMoreDialogBackButton);

            //            _View.CarryListsButton.Get<UIButton>().AddButtonClickFunction(_View.CarryListsButton, OnCarryListsButton);
            //            _View.CarryOpButton.Get<UIButton>().AddButtonClickFunction(_View.CarryOpButton, OnCarryOpButton);
            //            _View.CarryResultButton.Get<UIButton>().AddButtonClickFunction(_View.CarryResultButton, OnCarryResultButton);

            _View.FX_GeRenButton.Get<UIButton>().AddButtonClickFunction(_View.FX_GeRenButton, OnFX_GeRenButton);
            _View.FX_ClubButton.Get<UIButton>().AddButtonClickFunction(_View.FX_ClubButton, OnFX_ClubButton);
            _View.FX_MePokerButton.Get<UIButton>().AddButtonClickFunction(_View.FX_MePokerButton, OnFX_MePokerButton);

            _View.FX_ClubPokerDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.FX_ClubPokerDialogBackButton, OnFX_ClubPokerDialogBackButton);
            _View.ClubManagerDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.ClubManagerDialogBackButton, OnClubManagerDialogBackButton);

            _View.ClubManagerOpenPokerButton.Get<UIButton>().AddButtonClickFunction(_View.ClubManagerOpenPokerButton, OnClubManagerOpenPokerButton);
            _View.ClubManagerTeamButton.Get<UIButton>().AddButtonClickFunction(_View.ClubManagerTeamButton, OnClubManagerTeamButton);
            _View.ClubManagerCtrlButton.Get<UIButton>().AddButtonClickFunction(_View.ClubManagerCtrlButton, OnClubManagerCtrlButton);
            _View.ClubManagerListsButton.Get<UIButton>().AddButtonClickFunction(_View.ClubManagerListsButton, OnClubManagerListsButton);
            _View.NotOpenButton01.Get<UIButton>().AddButtonClickFunction(_View.NotOpenButton01, OnNotOpenButton);
            _View.NotOpenButton02.Get<UIButton>().AddButtonClickFunction(_View.NotOpenButton02, OnNotOpenButton);
            _View.GiveFundListsDialogBackButton.Get<UIButton>().AddButtonClickFunction(_View.GiveFundListsDialogBackButton, OnGiveFundListsDialogBackButton);
            _View.GiveFundListsButton.Get<UIButton>().AddButtonClickFunction(_View.GiveFundListsButton, OnGiveFundListsButton);

            //            _View.MePokerButton.Get<UIButton>().AddButtonClickFunction(_View.MePokerButton, OnMePokerButton);
            //            _View.AllPokerButton.Get<UIButton>().AddButtonClickFunction(_View.AllPokerButton, OnAllPokerButton);

            _View.ClubOpenPokerButton.Get<UIButton>().AddButtonClickFunction(_View.ClubOpenPokerButton, OnClubOpenPokerButton);


            //-------钱包
            _View.WalletDialogView.WalletBackButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletBackButton, OnWalletBackButton);
            _View.WalletDialogView.BTCButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.BTCButton, OnBTCButton);
            _View.WalletDialogView.BCHButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.BCHButton, OnBCHButton);
            _View.WalletDialogView.LTCButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.LTCButton, OnLTCButton);
            _View.WalletDialogView.ETHButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.ETHButton, OnETHButton);
            _View.WalletDialogView.DASHButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.DASHButton, OnDASHButton);

            _View.WalletDialogView.MaiRuButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.MaiRuButton, OnMaiRuButton);
            _View.WalletDialogView.MaiChuButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.MaiChuButton, OnMaiChuButton);
            _View.WalletDialogView.ChongBiButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.ChongBiButton, OnChongBiButton);
            _View.WalletDialogView.TiBiButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.TiBiButton, OnTiBiButton);

            _View.WalletDialogView.CBObjCloseButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.CBObjCloseButton, OnCBObjCloseButton);
            _View.WalletDialogView.CBCopyButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.CBCopyButton, OnCBCopyButton);
            _View.WalletDialogView.TBObjCloseButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.TBObjCloseButton, OnTBObjCloseButton);
            _View.WalletDialogView.TBAllButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.TBAllButton, OnTBAllButton);
            _View.WalletDialogView.TBOkButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.TBOkButton, OnTBOkButton);

            _View.WalletDialogView.GoumaiObjCloseButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.GoumaiObjCloseButton, OnGoumaiObjCloseButton);
            _View.WalletDialogView.MaiChuObjCloseButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.MaiChuObjCloseButton, OnMaiChuObjCloseButton);

            _View.WalletDialogView.WalletLogsButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsButton, OnWalletLogsButton);
            _View.WalletDialogView.GMAllButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.GMAllButton, OnGMAllButton);
            _View.WalletDialogView.GMOkButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.GMOkButton, OnGMOkButton);
            _View.WalletDialogView.MCAllButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.MCAllButton, OnMCAllButton);
            _View.WalletDialogView.MCOkButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.MCOkButton, OnMCOkButton);
            _View.WalletDialogView.RefreshButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.RefreshButton, OnRefreshButton);
            _View.WalletDialogView.WalletLogsBackButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsBackButton, OnWalletLogsBackButton);

            _View.WalletDialogView.WalletLogsCBButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsCBButton, OnWalletLogsCBButton);
            _View.WalletDialogView.WalletLogsTBButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsTBButton, OnWalletLogsTBButton);
            _View.WalletDialogView.WalletLogsMRButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsMRButton, OnWalletLogsMRButton);
            _View.WalletDialogView.WalletLogsMCButton.Get<UIButton>().AddButtonClickFunction(_View.WalletDialogView.WalletLogsMCButton, OnWalletLogsMCButton);


            _View.Editor_BackButton.Get<UIButton>().AddButtonClickFunction(_View.Editor_BackButton, OnEditor_BackButton);
            _View.Editor_SaveButton.Get<UIButton>().AddButtonClickFunction(_View.Editor_SaveButton, OnEditor_SaveButton);
            _View.Editor_ChangeHeadImageButton.gameObject.Get<UIButton>().AddButtonClickFunction(_View.Editor_ChangeHeadImageButton.gameObject, OnEditor_ChangeHeadImageButton);
            _View.ChooseGenderButton.Get<UIButton>().AddButtonClickFunction(_View.ChooseGenderButton, OnChooseGenderButton);
            _View.Editor_ChooseAreaButton.Get<UIButton>().AddButtonClickFunction(_View.Editor_ChooseAreaButton, OnEditor_ChooseAreaButton);
        }

        protected internal override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            if (Const.IsJoinClubDialog)
            {
                OnGongGaoButtonClicked(null);
            }
            else
            {
                ChangeTableSprite();
                _View.FaXianButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_find_mousefouse@2x");
                ShowDialog(_View.FX_MidleDialog);
            }
            StartCoroutine("RequestData");
        }

        IEnumerator GetMeWXSprite()
        {
            WWW www = new WWW(GameManager.ClientPlayer.Avatar);
            yield return www;
            if (www.texture != null)
            {
                GameManager.ClientPlayer.HeadSprite = PokerUtility.GetTexture2DToSprite(www.texture);
                StartCoroutine(UpdateUserInfo());
                www.Dispose();
            }
            StopCoroutine("GetWXClubHeadImage");
        }

        IEnumerator RequestData()
        {
            yield return new WaitForSeconds(0.1f);
            if (!Const.IsJoinClubDialog)
            {
                OnFX_GeRenButton(null);
            }
            yield return new WaitForSeconds(0.1f);
            ClientEvent_Hall.Instance.RequestUserInfo();
            Const.IsJoinClubDialog = false;
            yield return new WaitForSeconds(0.1f);
            ClientEvent_Hall.Instance.RequestGetMessage();
            _View.VersionText.text = string.Format("当前版本：{0}", Application.version);
        }

        public void SetHeadSprite()
        {
            if (GameManager.ClientPlayer != null)
            {
                if (GameManager.ClientPlayer.HeadSprite == null)
                {
                    if (GameManager.ClientPlayer.Avatar.StartsWith("http"))
                    {
                        StartCoroutine(GetMeWXSprite());
                    }
                    else
                    {
                        GameManager.ClientPlayer.HeadSprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(GameManager.ClientPlayer.Gender));
                        StartCoroutine(UpdateUserInfo());
                    }
                }
            }
        }

        protected internal override void OnClose(object userData)
        {
            base.OnClose(userData);
        }

        /// <summary>
        /// 设置时间显示
        /// </summary>
        /// <param name="msg">Message.</param>
        public void SetDateTimeText(string msg)
        {
            //            _View.DateTimeText.text = msg;

            if (_View.CB_CreateClubDialog.activeInHierarchy)
            {
                _View.CreateClubAreaText.text = string.Format("中国-{0}-{1}", Const.Province, Const.City).Replace("省", "");
            }

            SetMeMainUIData();

            if (_View.CB_MyClubMessageDialog.activeInHierarchy && _IsChangeArea)
            {
                _View.My_AreaText.text = string.Format("中国-{0}-{1}", Const.Province, Const.City).Replace("省", "");
            }

            if (_View.EditorUserMessageDialog.activeInHierarchy)
            {
                _View.AreaText.text = string.Format("中国-{0}-{1}", Const.Province, Const.City).Replace("省", "");
            }
        }

        private void SetCreateRoomStatus()
        {
            string people = string.Format("[{0}人桌] ", _CurrPeopleNumber);
            string carry = string.Format("[最小带入{0} | 最大带入{1}] ", _CurrMinCarry, _CurrMaxCarry);
            string totalCarry = "";
            if (_CurrTotalCarryNumber < 0)
            {
                totalCarry = string.Format("[总带入：无上限] ");
            }
            else
            {
                totalCarry = string.Format("[总带入：{0}] ", _CurrTotalCarryNumber);
            }
            string ante = string.Format("[Ante：{0}] ", _CurrAnteNumber);
            string straddle = "";
            string auto = "";
            string two = "";
            string ip = "";
            string gps = "";
            if (_View.StraddleToggle.isOn)
            {
                straddle = "[开启强制Straddle] ";
            }
            if (_View.AutoPokerToggle.isOn)
            {
                auto = "[开启自动埋牌] ";
            }
            if (_View.TwoSevenToggle.isOn)
            {
                two = "[开启2/7玩法] ";
            }
            if (_View.IPToggle.isOn)
            {
                ip = "[开启IP限制] ";
            }
            if (_View.GPSToggle.isOn)
            {
                gps = "[开启GPS限制] ";
            }
            _View.CreateRoomStatusText.text = string.Format("{0}{1}\n{2}{3}\n{4}{5}{6}{7}{8}", people, carry, totalCarry, ante, straddle, auto, two, ip, gps).ToReplaceSpace();
        }

        #region -------------  子Item对象数据设置 Prefab

        /// <summary>
        /// 1-个人，俱乐部  2-我的
        /// </summary>
        private int _CurrGetRoomOp;

        public void SetRoomListsItemData(string resp)
        {
            if (_RoomListsItemDataLists != null)
            {
                _RoomListsItemDataLists.Clear();
            }

            var jsonData = LitJson.JsonMapper.ToObject(resp);
            if (jsonData["list"] == null)
            {
                if (!_View.FX_ClubPokerDialog.activeInHierarchy && !_View.ClubManagerDialog.activeInHierarchy)
                {
                    ShowDialog(_View.FX_MidleDialog);
                }
                InitRoomListsGridScroller();
                return;
            }
            int count = jsonData["list"].Count;
            if (count > 0)
            {
                if (!_View.FX_ClubPokerDialog.activeInHierarchy && !_View.ClubManagerDialog.activeInHierarchy)
                {
                    ShowDialog(_View.FX_MidleDialog);
                }
                for (int i = 0; i < count; i++)
                {
                    var list = jsonData["list"][i];
                    RoomListsItemData data = new RoomListsItemData();
                    data.Id = (int)list["id"];
                    data.RoomName = list["name"].ToString();
                    data.BigBlind = (int)list["big_blind"];
                    data.SmallBlind = (int)list["small_blind"];
                    data.MinCarry = (int)list["min_carry"];
                    data.MaxCarry = (int)list["max_carry"];
                    data.MaxPeoPle = (int)list["max"];
                    data.Ante = (int)list["ante"];
                    data.Invitecode = (int)list["invitecode"];
                    data.CreateUserId = (int)list["create_userId"];
                    data.Avatar = list["create_avatar"].ToString();
                    data.Gender = (int)list["create_gender"];
                    data.Expire = (int)list["expire"];
                    data.Status = (int)list["status"];
                    data.CircleId = (int)list["circleid"];
                    data.NodeServer = list["node_server"].ToString();
                    data.RoomType = (int)list["rooms_type_id"];
                    data.OnLine = (int)list["online"];
                    data.CreateUserName = list["create_username"].ToString();
                    data.Expired = (int)list["remain"];
                    data.IsInsurance = (bool)list["insurance"];
                    data.IsGPS = (bool)list["gps"];
                    data.IsIP = (bool)list["ip"];
                    data.SitDown = (bool)list["sitdown"];
                    data.Create_type = (int)list["create_type"];

                    if (_CurrGetRoomOp == 1)
                    {
                        if (data.CircleId == 0 && _View.GridScroller_FaXian.gameObject.activeInHierarchy)
                        {
                            _RoomListsItemDataLists.Add(data);
                        }
                        else if (_View.FX_ClubPokerDialog.activeInHierarchy || _View.ClubManagerDialog.activeInHierarchy)
                        {
                            if (data.CircleId > 0 || data.CircleId == -1)
                            {
                                if (_View.FX_ClubPokerDialog.activeInHierarchy)
                                {
                                    _RoomListsItemDataLists.Add(data);
                                }
                                else
                                {
                                    _RoomListsItemDataLists.Add(data);
                                }
                            }
                        }
                    }
                    else if (_CurrGetRoomOp == 2)
                    {
                        _RoomListsItemDataLists.Add(data);
                    }

                }
                InitRoomListsGridScroller();
            }
        }

        private void InitRoomListsGridScroller()
        {
            if (_View.GridScroller_FaXian.gameObject.activeInHierarchy)
            {
                _View.GridScroller_FaXian.Init(ScrollRectBackFunction_Found, _RoomListsItemDataLists.Count, _View.UIRoomListsItemPrefab.transform);
            }
            else if (_View.GridScroller_ClubPoker.gameObject.activeInHierarchy)
            {
                _View.ClubPokerNumberText.text = string.Format("共有{0}个牌局", _RoomListsItemDataLists.Count);
                _View.GridScroller_ClubPoker.Init(ScrollRectBackFunction_Found, _RoomListsItemDataLists.Count, _View.UIRoomListsItemPrefab.transform);
            }
            else if (_View.GridScroller_Team.gameObject.activeInHierarchy)
            {
                _View.GridScroller_Team.Init(ScrollRectBackFunction_Found, _RoomListsItemDataLists.Count, _View.UIRoomListsItemPrefab.transform);
            }
        }

        private void ScrollRectBackFunction_Found(Transform trans, int index)
        {
            SetRoomListsItemData(trans.gameObject, _RoomListsItemDataLists[index]);
        }

        private void SetRoomListsItemData(GameObject go, RoomListsItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIRoomListsItemView>();
                if (view.Data != null)
                {
                    view.Data = null;
                }
                view.Data = data;
                view.BlindText.text = string.Format("{0}/{1}", data.SmallBlind, data.BigBlind);
                view.ExpireTimeText.text = string.Format("{0}m", data.Expire);
                view.PeopleNumberText.text = string.Format("{0}/{1}", data.OnLine, data.MaxPeoPle);
                view.RoomNameText.text = data.RoomName;
                view.ExpiredText.text = string.Format("{0}分钟结束", (int)(data.Expired / 60));
                view.AnteText.text = data.Ante.ToString();
                view.MinCarryText.text = data.MinCarry.ToString();
                view.InsuranceObj.SetActive(data.IsInsurance);
                if (data.IsGPS || data.IsIP)
                {
                    view.GPSOpenObj.SetActive(true);
                }
                else
                {
                    view.GPSOpenObj.SetActive(false);
                }
                view.PlayerHeadImage.gameObject.SetActive(false);
                view.ClubImage.gameObject.SetActive(false);
                if (data.Create_type == 1)
                {
                    view.CreatePlayerNameText.text = string.Format("(普通牌局)");
                }
                if (data.Create_type == 2)
                {
                    view.CreatePlayerNameText.text = string.Format("(俱乐部牌局)");
                    view.ClubImage.gameObject.SetActive(true);
                    view.ClubImage.sprite = AtlasMapping.Instance.GetAtlas("Other", "img_club@2x");
                }
                if (data.Create_type == 3)
                {
                    view.CreatePlayerNameText.text = string.Format("(联盟牌局)");
                    view.ClubImage.gameObject.SetActive(true);
                    view.ClubImage.sprite = AtlasMapping.Instance.GetAtlas("Other", "img_club@2x");
                }
                if (data.Create_type == 4)
                {
                    view.CreatePlayerNameText.text = string.Format("(官方牌局)");
                    view.PlayerHeadImage.gameObject.SetActive(true);
                    view.PlayerHeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", "guangfangicon");
                }
                if (data.Create_type == 1)
                {
                    view.PlayerHeadImage.gameObject.SetActive(true);
                    view.Avatar = data.Avatar;
                    if (data.Avatar.StartsWith("http"))
                    {
                        StartCoroutine(GetRoomPlayerHeadImage(view));
                    }
                    else
                    {
                        view.PlayerHeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(data.Gender));
                    }
                }

                go.Get<UIButton>().AddButtonClickFunction(go, OnRoomlistsItemClicked);
            }
        }

        IEnumerator GetRoomPlayerHeadImage(UIRoomListsItemView view)
        {
            WWW www = new WWW(view.Avatar);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                view.PlayerHeadImage.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetRoomPlayerHeadImage");
        }

        public void SetClubListsData(string resp, ClubOpType type)
        {
            var clubRet = JsonUtility.FromJson<ClubListsRet>(resp);

            switch (type)
            {
                case ClubOpType.Serach:
                    {
                        if (clubRet.list != null && clubRet.list.Length > 0)
                        {
                            _View.MyClubListsBackButton.SetActive(true);
                            _View.CB_JoinClubDialog.SetActive(false);
                            _View.CB_MyClubListsDialog.SetActive(true);
                        }
                        else
                        {
                            "未找到符合条件的俱乐部".ShowWarningTextTips();
                            return;
                        }
                    }
                    break;
                case ClubOpType.GetMyClubList:
                    {
                        if (!_View.FX_MidleDialog.activeInHierarchy)
                        {
                            if (clubRet.list != null && clubRet.list.Length > 0)
                            {
                                _View.MyClubListsBackButton.SetActive(false);
                                _View.CB_MyClubListsDialog.SetActive(true);
                                _View.CB_CreateClubDialog.SetActive(false);
                                _View.CB_WeiJiaRuDialog.SetActive(false);
                            }
                            else
                            {
                                _View.CB_MyClubListsDialog.SetActive(false);
                                _View.CB_WeiJiaRuDialog.SetActive(true);
                                return;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            _CurrClubOpType = type;
            if (_ClubListItemDataLists != null)
            {
                _ClubListItemDataLists.Clear();
            }
            int count = clubRet.list.Length;
            for (int i = 0; i < count; i++)
            {
                if (_View.FX_MidleDialog.activeInHierarchy)
                {
                    if (clubRet.list[i].alliance_room_count == 0 && clubRet.list[i].room_status == 0)
                    {
                        continue;
                    }
                }
                _ClubListItemDataLists.Add(clubRet.list[i]);
            }
            InitClubListsGridScroller();
        }

        private void InitClubListsGridScroller()
        {
            if (!_View.FX_MidleDialog.activeInHierarchy)
            {
                _View.GridScroller_ClubList.Init(ScrollRectBackFunction_Club, _ClubListItemDataLists.Count, _View.UIClubListsItemPrefab.transform);
            }
            else
            {
                _View.GridScroller_FaXianClub.Init(ScrollRectBackFunction_Club, _ClubListItemDataLists.Count, _View.UIClubListsItemPrefab.transform);
            }
        }

        private void ScrollRectBackFunction_Club(Transform trans, int index)
        {
            SetClubListsItemData(trans.gameObject, _ClubListItemDataLists[index]);
        }

        private void SetClubListsItemData(GameObject go, ClubListItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubListItemView>();
                view.ClubListItemData = data;
                view.ClubNameText.text = data.name;
                view.AreaText.text = data.area_code;
                view.PeopleText.text = string.Format("{0}/{1}", data.member_total + 1, data.max_member_total);
                if (_CurrClubOpType == ClubOpType.GetMyClubList)
                {
                    view.SearchImageTips.SetActive(false);
                    view.CreateButton.SetActive(true);
                    view.StateText.text = "牌局管理";
                    if (_View.GridScroller_FaXianClub.gameObject.activeInHierarchy)
                    {
                        int count = data.alliance_room_count + (data.room_status > 0 ? 1 : 0);
                        view.StateText.text = string.Format("进行中{0}个", count);
                        data.room_status = -1;
                    }
                }
                else if (_CurrClubOpType == ClubOpType.Serach)
                {
                    view.StateText.text = "";
                    view.SearchImageTips.SetActive(true);
                    view.CreateButton.SetActive(false);
                }
                if (!_View.FX_MidleDialog.activeInHierarchy)
                {
                    go.Get<UIButton>().AddButtonClickFunction(go, OnClublistsItemClicked);
                }
                view.CreateButton.Get<UIButton>().AddButtonClickFunction(go, OnItemCreateButton);

                if (data.avatar.StartsWith("http"))
                {
                    UIHeadImageData headData = new UIHeadImageData();
                    headData.Avatar = data.avatar;
                    headData.HeadImage = view.ClubImage;
                    StartCoroutine(GetClubHeadSprite(headData));
                }
                else
                {
                    view.ClubImage.sprite = AtlasMapping.Instance.GetAtlas("Other", "img_club@2x");
                }
            }
        }

        IEnumerator GetClubHeadSprite(UIHeadImageData data)
        {
            WWW www = new WWW(data.Avatar);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                data.HeadImage.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetClubHeadSprite");
        }

        public void SetClubPeopleListsData(string resp)
        {
            if (_ClubPeopleItemDataLists != null)
            {
                _ClubPeopleItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<ClubDetailsRet>(resp);
            if (clubRet.club != null)
            {
                var createData = new MemberInfo();
                createData = clubRet.club.create_user;
                createData.Job = 1;
                _ClubPeopleItemDataLists.Add(createData);
                if (createData.id == GameManager.ClientPlayer.UserId)
                {
                    GameManager.ClientPlayer.CurrClubJob = 1;
                }
                if (clubRet.club.members != null && clubRet.club.members.Length > 0)
                {
                    int count = clubRet.club.members.Length;
                    for (int i = 0; i < count; i++)
                    {
                        var memberData = new MemberInfo();
                        memberData = clubRet.club.members[i].player;
                        memberData.Job = clubRet.club.members[i].manager ? 2 : 3;
                        _ClubPeopleItemDataLists.Add(memberData);
                        if (memberData.id == GameManager.ClientPlayer.UserId && memberData.Job == 2)
                        {
                            GameManager.ClientPlayer.CurrClubJob = 2;
                        }
                        if (memberData.id == GameManager.ClientPlayer.UserId && memberData.Job == 3)
                        {
                            GameManager.ClientPlayer.CurrClubJob = 3;
                        }
                    }
                }
                if (_View.MyClubPeopleListsDialog.activeInHierarchy)
                {
                    InitClubPeopleListsGridScroller();
                }
                if (_View.GiveFundListsDialog.activeInHierarchy)
                {
                    InitGiveFundListsGridScroller();
                }
            }
        }

        private void InitClubPeopleListsGridScroller()
        {
            _View.GridScroller_ClubPeopleList.Init(ScrollRectBackFunction_ClubPeople, _ClubPeopleItemDataLists.Count, _View.UIClubPeopleListsItemPrefab.transform);
        }

        private void ScrollRectBackFunction_ClubPeople(Transform trans, int index)
        {
            SetClubPeopleListsItemData(trans.gameObject, _ClubPeopleItemDataLists[index], index);
        }

        private void SetClubPeopleListsItemData(GameObject go, MemberInfo data, int index)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubPeopleListItemView>();
                view.MemberInfo = data;
                view.RankText.text = (index + 1).ToString();
                view.CreateLogo.SetActive(data.Job == 1);
                view.ManagerLogo.SetActive(data.Job == 2);
                if (view.MemberInfo.Job == 1)
                {
                    view.UpButton.SetActive(false);
                    view.DownButton.SetActive(false);
                    view.DeleteButton.SetActive(false);
                }
                else
                {
                    if (GameManager.ClientPlayer.CurrClubJob == 1)
                    {
                        view.UpButton.SetActive(true);
                        view.DownButton.SetActive(true);
                        view.DeleteButton.SetActive(true);
                    }
                    else if (GameManager.ClientPlayer.CurrClubJob == 2)
                    {
                        view.UpButton.SetActive(false);
                        view.DownButton.SetActive(false);
                        view.DeleteButton.SetActive(view.MemberInfo.Job != 2);
                    }
                    else if (GameManager.ClientPlayer.CurrClubJob == 3)
                    {
                        view.UpButton.SetActive(false);
                        view.DownButton.SetActive(false);
                        view.DeleteButton.SetActive(false);
                    }
                }
                //如果是自己，把操作按钮隐藏
                if (view.MemberInfo.id == GameManager.ClientPlayer.UserId)
                {
                    view.UpButton.SetActive(false);
                    view.DownButton.SetActive(false);
                    view.DeleteButton.SetActive(false);
                }
                view.PlayerNameText.text = data.nick_name;
                view.LoginTimeText.text = data.last_login.ToTimeSpanCountDown();
                view.PlayTotalNumberText.text = data.total_hand.ToString();
                if (data.avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXClubHeadImage(view));
                }
                else
                {
                    view.HeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(data.gender));
                }
                go.Get<UIButton>().AddButtonClickFunction(go, OnClubPeoplelistsItemClicked);
                view.UpButton.Get<UIButton>().AddButtonClickFunction(go, OnClubPeoplelistsItemUpButtonClicked);
                view.DownButton.Get<UIButton>().AddButtonClickFunction(go, OnClubPeoplelistsItemDownButtonClicked);
                view.DeleteButton.Get<UIButton>().AddButtonClickFunction(go, OnClubPeoplelistsItemDeleteButtonClicked);
                //                view.GiveFundButton.Get<UIButton>().AddButtonClickFunction(go, OnGiveFundButton);
            }
        }

        private void InitGiveFundListsGridScroller()
        {
            _View.GridScroller_GiveFundLists.Init(ScrollRectBackFunction_GiveFundLists, _ClubPeopleItemDataLists.Count, _View.UIGiveFundListsItemPrefab.transform);
        }

        private void ScrollRectBackFunction_GiveFundLists(Transform trans, int index)
        {
            SetGiveFundListsItemData(trans.gameObject, _ClubPeopleItemDataLists[index]);
        }

        private void SetGiveFundListsItemData(GameObject go, MemberInfo data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubPeopleListItemView>();
                view.MemberInfo = data;
                view.PlayerNameText.text = string.Format("{0} (ID:{1})", data.nick_name, data.id);
                if (data.avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXClubHeadImage(view));
                }
                else
                {
                    view.HeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(data.gender));
                }
                go.Get<UIButton>().AddButtonClickFunction(go, OnGiveFundButton);
            }
        }

        IEnumerator GetWXClubHeadImage(UIClubPeopleListItemView view)
        {
            WWW www = new WWW(view.MemberInfo.avatar);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                view.HeadImage.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetWXClubHeadImage");
        }

        public void SetApplyClubPeopleListsData(string resp)
        {
            if (_ApplyClubPeopleItemDataLists != null)
            {
                _ApplyClubPeopleItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<ApplyClubPeopleListsRet>(resp);
            if (clubRet.list.Length > 0)
            {
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    _ApplyClubPeopleItemDataLists.Add(clubRet.list[i].content);
                }
            }
            InitApplyClubPeopleListsGridScroller();
            _View.MeTipsObj.SetActive(clubRet.list.Length > 0);
            _View.MessageTipsObj.SetActive(clubRet.list.Length > 0);
        }

        private void InitApplyClubPeopleListsGridScroller()
        {
            _View.GridScroller_ApplyClubPeople.Init(ScrollRectBackFunction_ApplyClubPeople, _ApplyClubPeopleItemDataLists.Count, _View.UIApplyPeopleItemPrefab.transform);
        }

        private void ScrollRectBackFunction_ApplyClubPeople(Transform trans, int index)
        {
            SetApplyClubPeopleListsItemData(trans.gameObject, _ApplyClubPeopleItemDataLists[index]);
        }

        private void SetApplyClubPeopleListsItemData(GameObject go, ApplyMemberInfo data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIApplyPeopleItemView>();
                view.PlayerNameText.text = data.player.nick_name;
                view.TipsText.text = string.Format("申请加入”{0}“俱乐部", data.name);
                view.MemberInfo = data;
                view.CreateTimeText.text = data.created;
                view.OkButton.Get<UIButton>().AddButtonClickFunction(go, OnApplyClubPeopleOkClicked);
                view.CancelButton.Get<UIButton>().AddButtonClickFunction(go, OnApplyClubPeopleCancelClicked);
            }
        }

        public void SetChargesListsData(string resp)
        {
            var clubRet = JsonUtility.FromJson<GetChargeData>(resp);
            if (clubRet.list.Length != 0)
            {
                int count = clubRet.list.Length;
                if (count == 5)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var view = _View.BuyDiamondButtons[i].Get<UIChargeItemView>();
                        view.PriceText.text = string.Format("¥{0}", clubRet.list[i].price * 0.01f);
                        view.TipsText.text = clubRet.list[i].name;
                        view.Data = clubRet.list[i];
                    }
                }
                if (!_View.ChargeDialog.activeInHierarchy)
                {
                    GameManager.Instance.SetTweenAnimation(_View.ChargeDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                }
            }
        }

        #region 商店功能暂不开启

        /// <summary>
        /// 设置商城数据
        /// </summary>
        /// <param name="resp">Resp.</param>
        public void SetShopGoodsListsData(string resp)
        {
            if (_ShopGoodsItemDataLists != null)
            {
                _ShopGoodsItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<GetGoodsData>(resp);
            if (clubRet.list.Length != 0)
            {
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    _ShopGoodsItemDataLists.Add(clubRet.list[i]);
                }
                if (!_View.ShopGiftDialog.activeInHierarchy)
                {
                    GameManager.Instance.SetTweenAnimation(_View.ShopGiftDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                }
                InitShopGoodsListsGridScroller();
            }
        }

        private void InitShopGoodsListsGridScroller()
        {
            _View.GridScroller_Shop.Init(ScrollRectBackFunction_ShopGoods, _ShopGoodsItemDataLists.Count, _View.ShopGoodsItemPrefab.transform);
        }

        private void ScrollRectBackFunction_ShopGoods(Transform trans, int index)
        {
            SetShopGoodsListsItemData(trans.gameObject, _ShopGoodsItemDataLists[index]);
        }

        private void SetShopGoodsListsItemData(GameObject go, GoodsData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIShopGoodItemView>();
                view.GoodsImage.sprite = AtlasMapping.Instance.GetAtlas("GiftIcon", string.Format("10000{0}", data.id));
                view.GoodsImage.SetNativeSize();
                view.GoodsNameText.text = data.name;
                view.TipsText.text = data.goods_describe.ToReplaceSpace();
                view.Data = data;
                view.PriceText.text = data.price.ToString();
                view.BuyButton.Get<UIButton>().AddButtonClickFunction(go, OnBuuyOkClicked);
            }
        }

        #endregion

        /// <summary>
        /// 设置历史统计数据
        /// </summary>
        /// <param name="resp">Resp.</param>
        public void SetHistoryListsData(string resp)
        {
            var clubRet = JsonUtility.FromJson<GetHistorysData>(resp);
            if (_View.GetMyRoomDialog.activeInHierarchy)
            {
                _View.GetMyRoomTotalRoundText.text = clubRet.list[0].total_room.ToString();
                _View.GetMyRoomTotalHandsText.text = clubRet.list[0].total_hand.ToString();
                _View.GetMyRoomTotalChipsText.text = clubRet.list[0].total_win.ToString();
                _View.GetMyRoomTotalInBoundRateText.text = string.Format("{0}%", clubRet.list[0].total_inbound);
                return;
            }

            if (_HistoryItemDataLists != null)
            {
                _HistoryItemDataLists.Clear();
            }
            if (clubRet.list.Length != 0)
            {
                _View.HistoryNoneDialog.SetActive(false);
                _View.HistoryHasDialog.SetActive(true);
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    if (clubRet.list[i].day != 0)
                    {
                        _HistoryItemDataLists.Add(clubRet.list[i]);
                    }
                }
                InitHistoryListsGridScroller();
            }
            else
            {
                _View.HistoryNoneDialog.SetActive(true);
                _View.HistoryHasDialog.SetActive(false);
            }
        }

        private void InitHistoryListsGridScroller()
        {
            _View.GridScroller_History.Init(ScrollRectBackFunction_SummaryLists, _HistoryItemDataLists.Count, _View.HistoryItemPrefab.transform);
        }

        private void ScrollRectBackFunction_SummaryLists(Transform trans, int index)
        {
            SetHistoryListsItemData(trans.gameObject, _HistoryItemDataLists[index]);
        }

        private void SetHistoryListsItemData(GameObject go, HistorysData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIHistoryItemView>();
                if (data.total_win > 0)
                {
                    view.TotalChipsText.text = string.Format("<color=red>+{0}</color>", data.total_win);
                }
                else
                {
                    view.TotalChipsText.text = string.Format("<color=green>{0}</color>", data.total_win);
                }
                view.TotalHandText.text = string.Format("{0}手", data.total_hand);
                var temp = data.day.ToString().ToCharArray();
                if (temp.Length > 7)
                {
                    view.YearText.text = string.Format("{0}{1}{2}{3}", temp[0], temp[1], temp[2], temp[3]);
                    view.MonthText.text = string.Format("{0}{1}", temp[4], temp[5]);
                    view.DayText.text = string.Format("{0}{1}", temp[6], temp[7]);
                }
            }
        }

        public void SetNotesResp(string resp)
        {
            _CurrNotes = resp;
            SetNotesListsData();
        }

        private void SetNotesListsData()
        {
            if (_NotesItemDataLists != null)
            {
                _NotesItemDataLists.Clear();
            }
            var resp = LitJson.JsonMapper.ToObject(_CurrNotes);
            if (resp["list"] != null)
            {
                int count = resp["list"].Count;
                for (int i = 0; i < count; i++)
                {
                    var data = new NotesItemData();
                    data.id = (int)resp["list"][i]["id"];
                    data.status = (int)resp["list"][i]["status"];
                    data.title = resp["list"][i]["title"].ToString();
                    data.content = resp["list"][i]["content"].ToString();
                    data.image = resp["list"][i]["image"].ToString();
                    data.pub_at = resp["list"][i]["pub_at"].ToString();
                    data.end_at = resp["list"][i]["end_at"].ToString();
                    _NotesItemDataLists.Add(data);
                }
            }
            InitNotesListsGridScroller();
        }

        private void InitNotesListsGridScroller()
        {
            _View.GridScroller_Notes.Init(ScrollRectBackFunction_Notes, _NotesItemDataLists.Count, _View.UINotesItemPrefab.transform);
        }

        private void ScrollRectBackFunction_Notes(Transform trans, int index)
        {
            SetNotesListsItemData(trans.gameObject, _NotesItemDataLists[index]);
        }

        private void SetNotesListsItemData(GameObject go, NotesItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UINotesItemView>();
                view.New.SetActive(data.status == 1);
                view.Data = data;
                go.Get<UIButton>().AddButtonClickFunction(go, OnNotesItemClicked);

                if (view.Data.image.StartsWith("http"))
                {
                    StartCoroutine(GetNotesSprite(view));
                }
                else
                {
                    view.NotesImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "gonggaotu");
                }
            }
        }

        IEnumerator GetNotesSprite(UINotesItemView view)
        {
            WWW www = new WWW(view.Data.image);
            yield return www;
            if (www.texture != null)
            {
                view.NotesImage.sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                www.Dispose();
            }
            StopCoroutine("GetNotesSprite");
        }

        /// <summary>
        /// 设置我参与的房间数据
        /// </summary>
        /// <param name="resp">Resp.</param>
        public void SetGetMyRoomListsData(string resp)
        {
            if (_GetMyRoomItemDataLists != null)
            {
                _GetMyRoomItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<GetMyRoomsData>(resp);
            if (clubRet.list.Length != 0)
            {
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    _GetMyRoomItemDataLists.Add(clubRet.list[i]);
                }
                InitGetMyRoomListsGridScroller();
            }
        }

        private void InitGetMyRoomListsGridScroller()
        {
            _View.GridScroller_GetMyRoom.Init(ScrollRectBackFunction_GetMyRoomLists, _GetMyRoomItemDataLists.Count, _View.GetMyRoomItemPrefab.transform);
        }

        private void ScrollRectBackFunction_GetMyRoomLists(Transform trans, int index)
        {
            SetGetMyRoomListsItemData(trans.gameObject, _GetMyRoomItemDataLists[index]);
        }

        private GetMyRoomListsData _CurrGetMyRoomListsData;

        private void SetGetMyRoomListsItemData(GameObject go, GetMyRoomListsData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIGetMyRoonItemView>();
                view.RoomNameText.text = data.name;
                view.RoomBlindText.text = string.Format("{0}/{1}({2})", data.small_blind, data.big_blind, data.ante);
                view.RoomTimeText.text = string.Format("{0}分钟", data.expire);
                view.TotalText.text = data.win.ToString();
                //                ChangeColor(view.TotalText, data.win);
                if (data.win == 0)
                {
                    view.TotalText.gameObject.GetComponentInParent<Image>().color = Color.grey;
                }
                if (data.win > 0)
                {
                    view.TotalText.gameObject.GetComponentInParent<Image>().color = new Color(193f / 255f, 80f / 255f, 80f / 255f, 1f);
                    view.TotalText.text = string.Format("+{0}", data.win);
                }
                if (data.win < 0)
                {
                    view.TotalText.gameObject.GetComponentInParent<Image>().color = new Color(80f / 255f, 193f / 255f, 118f / 255f, 1f);
                }
                view.Data = data;
                if (data.create_type == 4)
                {
                    view.FromText.text = string.Format("来自 <color=yellow>官方</color> 的快速局");
                }
                else if (data.create_type == 3)
                {
                    view.FromText.text = string.Format("来自 联盟共享 的快速局");
                }
                else
                {
                    view.FromText.text = string.Format("来自 {0} 的快速局", data.create_user.nick_name);
                }


                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddSeconds(data.updated.ToString().ToInt64());
                view.DateText.text = dt.ToLocalTime().ToString("MM月");
                view.DateText02.text = dt.ToLocalTime().ToString("dd日");
                view.TimeText.text = dt.ToLocalTime().ToString("HH:mm");
                go.Get<UIButton>().AddButtonClickFunction(go, OnGetMyRoomItemClicked);
                view.HeadImage.sprite = GameManager.ClientPlayer.HeadSprite;
            }
        }

        public void SetGetMyRoomTableData(string resp)
        {
            if (_GetMyRoomTableItemDataLists != null)
            {
                _GetMyRoomTableItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<GetMyRoomTables>(resp);
            if (clubRet.table.game_players.Length != 0)
            {
                int count = clubRet.table.game_players.Length;
                int maxCarryIndex = 0;
                int tempCarry = 0;
                for (int i = 0; i < count; i++)
                {
                    if (clubRet.table.game_players[i].carry > tempCarry)
                    {
                        maxCarryIndex = i;
                        tempCarry = clubRet.table.game_players[i].carry;
                    }
                    _GetMyRoomTableItemDataLists.Add(clubRet.table.game_players[i]);
                }
                InitGetMyRoomTableGridScroller();
                if (clubRet.table.game_table.start_at == 0)
                {
                    _View.RoomTableTimeText.text = "0分";
                }
                else
                {
                    _View.RoomTableTimeText.text = (clubRet.table.game_table.end_at - clubRet.table.game_table.start_at).ToTime();
                }
                _View.GetMyRoomBaoXianStateObj.SetActive(_CurrGetMyRoomListsData.insurance);
                _View.GetMyRoomPeopleNumberText.text = string.Format("{0}人普通局", _CurrGetMyRoomListsData.max);

                _View.RoomTableTotalHandText.text = clubRet.table.game_table.hand.ToString();
                _View.RoomTableTotalCarryText.text = clubRet.table.game_table.carry.ToString();
                _View.RoomTableTotalInsuranceText.text = clubRet.table.game_table.insurance.ToString();
                _View.RoomTableBlindText.text = string.Format("{0}/{1}({2})", _CurrGetMyRoomListsData.small_blind, _CurrGetMyRoomListsData.big_blind, _CurrGetMyRoomListsData.ante);
                _View.RoomTableHeadImage.sprite = GameManager.ClientPlayer.HeadSprite;
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddSeconds(_CurrGetMyRoomListsData.updated.ToString().ToInt64());
                _View.RoomTableStartTimeText.text = dt.ToLocalTime().ToString("MM/dd/HH:mm");
                _View.RoomTableRoomNameText.text = _CurrGetMyRoomListsData.name;
                if (_CurrGetMyRoomListsData.create_type == 3)
                {
                    _View.RoomTableCreateUserNameText.text = string.Format("({0})", "联盟共享");
                }
                else if (_CurrGetMyRoomListsData.create_type == 4)
                {
                    _View.RoomTableCreateUserNameText.text = string.Format("({0})", "<color=yellow>官方</color>");
                }
                else
                {
                    _View.RoomTableCreateUserNameText.text = string.Format("({0})", _CurrGetMyRoomListsData.create_user.nick_name);
                }
                ChangeColor(_View.RoomTableTotalInsuranceText, clubRet.table.game_table.insurance);

                _View.RoomTableVP01Text.text = clubRet.table.game_players[count - 1].name;
                _View.RoomTableVP02Text.text = clubRet.table.game_players[0].name;
                _View.RoomTableVP03Text.text = clubRet.table.game_players[maxCarryIndex].name;
                if (clubRet.table.game_players[count - 1].avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXRoomTableVPHeadImage01(clubRet.table.game_players[count - 1].avatar, _View.RoomTableVP01Image));
                }
                else
                {
                    _View.RoomTableVP01Image.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(clubRet.table.game_players[count - 1].gender));
                }
                if (clubRet.table.game_players[0].avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXRoomTableVPHeadImage01(clubRet.table.game_players[0].avatar, _View.RoomTableVP02Image));
                }
                else
                {
                    _View.RoomTableVP02Image.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(clubRet.table.game_players[0].gender));
                }
                if (clubRet.table.game_players[maxCarryIndex].avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXRoomTableVPHeadImage01(clubRet.table.game_players[maxCarryIndex].avatar, _View.RoomTableVP03Image));
                }
                else
                {
                    _View.RoomTableVP03Image.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(clubRet.table.game_players[maxCarryIndex].gender));
                }
            }
        }

        IEnumerator GetWXRoomTableVPHeadImage01(string url, Image image)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                image.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetWXRoomTableVPHeadImage01");
        }

        private void InitGetMyRoomTableGridScroller()
        {
            _View.GridScroller_GetMyRoomTable.Init(ScrollRectBackFunction_GetMyRoomTable, _GetMyRoomTableItemDataLists.Count, _View.GetMyRoomTableItemPrefab.transform);
        }

        private void ScrollRectBackFunction_GetMyRoomTable(Transform trans, int index)
        {
            SetGetMyRoomTableItemData(trans.gameObject, _GetMyRoomTableItemDataLists[index], index + 1);
        }

        private void SetGetMyRoomTableItemData(GameObject go, GamePlayersData data, int index)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIGetMyRoonTableItemView>();
                view.IndexText.text = index.ToString();
                view.CarryText.text = string.Format("带入 {0}", data.carry);
                view.NameText.text = data.name;

                //                view.BaoXianText.text = data.insurance.ToString();
                //                ChangeColor(view.BaoXianText, data.insurance);
                //
                //                view.PaijuText.text = (data.chips - data.insurance - data.carry).ToString();
                //                ChangeColor(view.PaijuText, data.chips - data.insurance - data.carry);

                view.TotalText.text = (data.chips - data.carry).ToString();
                ChangeColor(view.TotalText, data.chips - data.carry);

                view.avatar = data.avatar;
                if (data.avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXRoomTableHeadImage(view));
                }
                else
                {
                    view.HeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(data.gender));
                }
            }
        }

        IEnumerator GetWXRoomTableHeadImage(UIGetMyRoonTableItemView view)
        {
            WWW www = new WWW(view.avatar);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                view.HeadImage.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetWXRoomTableHeadImage");
        }

        private void ChangeColor(Text go, int number)
        {
            if (number == 0)
            {
                go.color = Color.grey;
            }
            if (number > 0)
            {
                //                go.color = new Color(193f / 255f, 80f / 255f, 80f / 255f, 1f);
                go.color = Color.red;
                go.text = string.Format("+{0}", number);
            }
            if (number < 0)
            {
                //                go.color = new Color(80f / 255f, 193f / 255f, 118f / 255f, 1f);
                go.color = Color.green;
            }
        }

        public void SetFundListsData(string resp)
        {
            if (_GetFundListsData != null)
            {
                _GetFundListsData.Clear();
            }
            var clubRet = JsonUtility.FromJson<FundListsData>(resp);
            if (clubRet.list.Length != 0)
            {
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    _GetFundListsData.Add(clubRet.list[i]);
                }
            }
            InitFundListsGridScroller();
        }

        private void InitFundListsGridScroller()
        {
            _View.GridScroller_ClubFund.Init(ScrollRectBackFunction_FundLists, _GetFundListsData.Count, _View.UIClubFundListItemPrefab.transform);
        }

        private void ScrollRectBackFunction_FundLists(Transform trans, int index)
        {
            SetFundListsItemData(trans.gameObject, _GetFundListsData[index]);
        }

        private void SetFundListsItemData(GameObject go, GetFundListsData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubFundListItemView>();
                view.Text.text = string.Format("{0} 收到 {1} 发放的 {2} 战队基金", data.Member.nick_name, _CurrChooseClubListsItemData.create_user.nick_name, data.Amount);
            }
        }

        private void InitBaoXianMoreGridScroller()
        {
            _View.GridScroller_BaoXianMoreDialog.Init(ScrollRectBackFunction_BaoXianMore, _GetMyRoomTableItemDataLists.Count, _View.BaoXianMoreItemPrefab.transform);
        }

        private void ScrollRectBackFunction_BaoXianMore(Transform trans, int index)
        {
            SetBaoXianMoreItemData(trans.gameObject, _GetMyRoomTableItemDataLists[index]);
        }

        private void SetBaoXianMoreItemData(GameObject go, GamePlayersData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIGetMyRoonTableItemView>();
                view.NameText.text = data.name;
                view.BaoXianText.text = data.insurance.ToString();
                ChangeColor(view.BaoXianText, data.insurance);
                view.avatar = data.avatar;
                if (data.avatar.StartsWith("http"))
                {
                    StartCoroutine(GetWXRoomTableHeadImage(view));
                }
                else
                {
                    view.HeadImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(data.gender));
                }
            }
        }

        /// <summary>
        /// 设置俱乐部控制申请列表数据
        /// </summary>
        /// <param name="resp">Resp.</param>    
        public void SetClubCtrlData(string resp)
        {
            if (_ClubCtrlItemListsData != null)
            {
                _ClubCtrlItemListsData.Clear();
            }
            var clubRet = JsonUtility.FromJson<CtrlListsRet>(resp);
            if (clubRet.applys.Length > 0)
            {
                int count = clubRet.applys.Length;
                for (int i = 0; i < count; i++)
                {
                    var data = new CtrlItemData();
                    data.CtrlApplyMemberInfo = clubRet.applys[i];
                    int roomCount = clubRet.rooms.Length;
                    for (int j = 0; j < roomCount; j++)
                    {
                        if (clubRet.rooms[j].id == clubRet.applys[i].room_id)
                        {
                            data.CtrlRoomItem = clubRet.rooms[j];
                        }
                    }
                    _ClubCtrlItemListsData.Add(data);
                }
            }
            InitClubCtrlItemGridScroller();
        }

        private void InitClubCtrlItemGridScroller()
        {
            _View.GridScroller_Ctrl.Init(ScrollRectBackFunction_ClubCtrlItem, _ClubCtrlItemListsData.Count, _View.UIClubCtrlItemPrefab.transform);
        }

        private void ScrollRectBackFunction_ClubCtrlItem(Transform trans, int index)
        {
            SetClubCtrlItemData(trans.gameObject, _ClubCtrlItemListsData[index]);
        }

        private void SetClubCtrlItemData(GameObject go, CtrlItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubCtrlltemView>();
                view.PlayerNameText.text = string.Format("{0} (ID:{1})", data.CtrlApplyMemberInfo.name, data.CtrlApplyMemberInfo.player_id).ToReplaceSpace();
                view.Data = data;
                view.RoomNameText.text = data.CtrlRoomItem.name;
                view.PeopleText.text = string.Format("{0}/{1}", data.CtrlRoomItem.online, data.CtrlRoomItem.max);
                view.BlidText.text = string.Format("{0}/{1}", data.CtrlRoomItem.small_blind, data.CtrlRoomItem.big_blind);
                view.TimeText.text = string.Format("{0}m", data.CtrlRoomItem.expire);
                view.TotalCarryText.text = data.CtrlApplyMemberInfo.carry.ToString();
                view.TotalResultText.text = data.CtrlApplyMemberInfo.chips.ToString();
                view.CarryNumberText.text = data.CtrlApplyMemberInfo.apply_carry.ToString();
                view.DownTimeText.text = data.CtrlApplyMemberInfo.remain.ToString();
                view.SetDownTime(data.CtrlApplyMemberInfo.remain);
                view.OkButton.Get<UIButton>().AddButtonClickFunction(go, OnClubCtrlItemOkClicked);
                view.CnacelButton.Get<UIButton>().AddButtonClickFunction(go, OnClubCtrlItemCancelClicked);
            }
        }


        public void SetClubResultData(string resp)
        {
            if (_ClubHistroyItemDataLists != null)
            {
                _ClubHistroyItemDataLists.Clear();
            }
            var clubRet = JsonUtility.FromJson<ClubHistroyRet>(resp);
            if (clubRet.list.Length > 0)
            {
                int count = clubRet.list.Length;
                for (int i = 0; i < count; i++)
                {
                    _ClubHistroyItemDataLists.Add(clubRet.list[i]);
                }
            }
            InitClubResultItemGridScroller();
        }

        private void InitClubResultItemGridScroller()
        {
            _View.GridScroller_ClubHistroy.Init(ScrollRectBackFunction_ClubResultItem, _ClubHistroyItemDataLists.Count, _View.UIClubHistroyltemPrefab.transform);
        }

        private void ScrollRectBackFunction_ClubResultItem(Transform trans, int index)
        {
            SetClubResultItemData(trans.gameObject, _ClubHistroyItemDataLists[index]);
        }

        private void SetClubResultItemData(GameObject go, ClubHistroyItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIClubHistroyltemView>();
                view.PlayerNameText.text = string.Format("{0} (ID:{1})", data.player_name, data.player_id).ToReplaceSpace();
                view.Data = data;
                view.RoomNameText.text = data.room_name;
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddSeconds(data.created.ToString().ToInt64());
                view.TimeText.text = dt.ToLocalTime().ToString("MM-dd HH:mm");
                view.CarryText.text = data.carry.ToString();
            }
        }

        private string _CurrGetWalletLogsData;

        /// <summary>
        /// ---------------------------------------- 钱包明细
        /// </summary>
        /// <param name="resp">Resp.</param>
        public void SetWalletLogsData(string resp)
        {
            _CurrGetWalletLogsData = resp;
            if (_View.WalletDialogView.WalletLogsDialog.activeInHierarchy)
            {
                SetWalletLogsData(1);
            }
            else
            {
                SetWalletLogsData(2);
            }
        }

        /// <summary>
        /// //1--充值 2-体现 3-买 4-卖  0-所有
        /// </summary>
        /// <param name="type">Type.</param>
        private void SetWalletLogsData(int type)
        {
            if (_WalletLogsItemDataLists != null)
            {
                _WalletLogsItemDataLists.Clear();
            }
            var ret = JsonUtility.FromJson<WalletLogsListsRet>(_CurrGetWalletLogsData);
            if (ret.list.Length > 0)
            {
                int count = ret.list.Length;
                for (int i = 0; i < count; i++)
                {
                    if (type != 0)
                    {
                        if (ret.list[i].tag == type)
                        {
                            if (_View.WalletDialogView.WalletLogsDialog.activeInHierarchy)
                            {
                                _WalletLogsItemDataLists.Add(ret.list[i]);
                            }
                            else
                            {
                                if (ret.list[i].status == 0)
                                {
                                    _WalletLogsItemDataLists.Add(ret.list[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        _WalletLogsItemDataLists.Add(ret.list[i]);
                    }
                }
            }
            InitWalletLogsItemGridScroller();

            _View.WalletDialogView.WalletLogsCBButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.WalletDialogView.WalletLogsTBButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.WalletDialogView.WalletLogsMRButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.WalletDialogView.WalletLogsMCButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            switch (type)
            {
                case 1:
                    _View.WalletDialogView.WalletLogsCBButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
                    break;
                case 2:
                    _View.WalletDialogView.WalletLogsTBButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
                    break;
                case 3:
                    _View.WalletDialogView.WalletLogsMRButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
                    break;
                case 4:
                    _View.WalletDialogView.WalletLogsMCButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
                    break;
                default:
                    break;
            }
        }

        private void InitWalletLogsItemGridScroller()
        {
            if (_View.WalletDialogView.WalletLogsDialog.activeInHierarchy)
            {
                _View.WalletDialogView.GridScroller_WalletLogs.Init(ScrollRectBackFunction_WalletLogs, _WalletLogsItemDataLists.Count, _View.WalletDialogView.UIWalletLogsItemPrefab.transform);
            }
            else
            {
                _View.WalletDialogView.GridScroller_Wallets.Init(ScrollRectBackFunction_WalletLogs, _WalletLogsItemDataLists.Count, _View.WalletDialogView.UIWalletLogsItemPrefab.transform);
            }
        }

        private void ScrollRectBackFunction_WalletLogs(Transform trans, int index)
        {
            SetWalletLogsItemData(trans.gameObject, _WalletLogsItemDataLists[index]);
        }

        private void SetWalletLogsItemData(GameObject go, WalletLogsItemData data)
        {
            go.SetActive(data != null);
            if (data != null)
            {
                var view = go.Get<UIWalletlogsItemView>();
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddSeconds(data.create_at.ToString().ToInt64());
                view.TimeText.text = dt.ToLocalTime().ToString("MM月dd日 HH时mm分");
                view.OrderText.text = string.Format("订单号-{0}", data.id);
                view.TypeText.text = string.Format("数字货币类型：{0}", data.currency_id);
                view.GetMoneyText.text = "";
                view.SellChipsText.text = "";
                if (data.tag == 4)
                {
                    view.GetMoneyText.text = string.Format("获得货币金额：{0}", data.amount);
                    view.SellChipsText.text = string.Format("卖出筹码数量：{0}", data.balance);
                }
                if (data.tag == 3)
                {
                    view.GetMoneyText.text = string.Format("买入金额：{0}", data.amount);
                    view.SellChipsText.text = string.Format("获得筹码：{0}", data.balance);
                }
                if (data.tag == 1)
                {
                    view.GetMoneyText.text = string.Format("{0}", data.amount);
                }
                if (data.tag == 2)
                {
                    if (_View.WalletDialogView.WalletLogsDialog.activeInHierarchy)
                    {
                        view.SellChipsText.text = string.Format("{0}", data.amount);
                        if (data.status == 1)
                        {
                            view.GetMoneyText.text = string.Format("已完成");
                        }
                        else if (data.status == 2)
                        {
                            view.GetMoneyText.text = string.Format("已拒绝");
                        }
                        else
                        {
                            view.GetMoneyText.text = string.Format("处理中");
                        }
                    }
                    else
                    {
                        view.SellChipsText.text = string.Format("{0}", data.amount);
                        view.GetMoneyText.text = string.Format("处理中");

                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 解散俱乐部成功
        /// </summary>
        public void DissolutionClub()
        {
            OnMy_BackButton(null);
            _View.PublicTipsDialog.SetActive(false);
            ClientEvent_Hall.Instance.RequestGetMyClubLists();
        }

        private void ChangeTableSprite()
        {
            _View.GongGaoButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_news@2x");
            _View.YueJuButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_pk@2x");
            _View.FaXianButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_find@2x");
            _View.JuLeBuButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "tab_icon_club@2x");
            _View.MeButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "tab_icon_itsme@2x");
        }

        /// <summary>
        /// 显示UI界面
        /// </summary>
        /// <param name="go">Go.</param>
        private void ShowDialog(GameObject go)
        {
            _View.GongGaoDialog.SetActive(false);
            _View.YueJu_KuaiSuDialog.SetActive(false);
            _View.Me_MainDialog.SetActive(false);
            _View.FX_NoneDialog.SetActive(false);
            _View.FX_MidleDialog.SetActive(false);
            _View.YueJu_CreateMatchDialog.SetActive(false);
            _View.CM_HightSettingDialog.SetActive(false);
            _View.CM_HightSettingTipsDialog.SetActive(false);
            //            _View.FX_SmallDialog.SetActive(false);
            //            _View.FX_MaxDialog.SetActive(false);
            _View.CB_DontCreateClubDialog.SetActive(false);
            _View.CB_CreateClubDialog.SetActive(false);
            _View.CB_MyClubListsDialog.SetActive(false);
            _View.CB_MyClubMessageDialog.SetActive(false);
            _View.CB_OtherClubMessageDialog.SetActive(false);
            _View.CB_WeiJiaRuDialog.SetActive(false);
            _View.CB_AddClubDialog.SetActive(false);
            _View.CB_JoinClubDialog.SetActive(false);
            _View.Me_SummaryDialog.SetActive(false);
            _View.HistoryDialog.SetActive(false);
            _View.ShopGiftDialog.SetActive(false);
            _View.ChargeDialog.SetActive(false);
            _View.SettingDialog.SetActive(false);
            _View.MyClubPeopleListsDialog.SetActive(false);
            _View.PublicTipsDialog.SetActive(false);
            _View.ApplyClubPeopleListsDialog.SetActive(false);
            _View.TextTipsDialog.SetActive(false);
            _View.AnalysisDialog.SetActive(false);
            _View.AbountWeDialog.SetActive(false);
            _View.NotesDialog.SetActive(false);
            _View.GetMyRoomDialog.SetActive(false);
            _View.GetMyRoomTableDialog.SetActive(false);
            _View.MyClubFundListsDialog.SetActive(false);
            _View.GiveFundDialog.SetActive(false);
            _View.GridScroller_FaXian.gameObject.SetActive(true);
            _View.GridScroller_FaXianClub.gameObject.SetActive(false);
            _View.FX_ClubPokerDialog.SetActive(false);
            //            _View.FX_StateImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "gerenjuann");
            _View.BaoXianMoreDialog.SetActive(false);
            _View.ClubManagerDialog.SetActive(false);
            _View.GiveFundDialog.SetActive(false);
            _View.GiveFundListsDialog.SetActive(false);
            _IsCreatePokerTeam = false;

            _View.WalletDialog.SetActive(false);
            _View.WalletDialogView.WalletLogsDialog.SetActive(false);
            _View.ChooseTypeDialog.SetActive(false);
            _View.EditorUserMessageDialog.SetActive(false);
            if (go != null)
            {
                go.SetActive(true);
            }
        }

        /// <summary>
        /// 赋值 主面板UI数据
        /// </summary>
        private void SetMeMainUIData()
        {
            var player = GameManager.ClientPlayer;
            if (_View.Me_MainDialog.activeInHierarchy)
            {
                _View.Me_NameText.text = player.Name;
                _View.Me_IDText.text = string.Format("ID:{0}", player.UserId.ToString());
                _View.GoldText.text = player.Balance.ToString();
                _View.DiamondText.text = player.Diamond.ToString();
                _View.Me_AreaText.text = player.AreaCode;
                _View.Me_CardLevelText.text = PokerUtility.GetVIPCardName(player.VIP);
            }
            if (_View.ShopGiftDialog.activeInHierarchy)
            {
                _View.ShopGoldText.text = player.Balance.ToString();
                _View.CardLevelText.text = PokerUtility.GetVIPCardName(player.VIP);
                _View.ShopDiamondText.text = player.Diamond.ToString();
            }
            if (_View.ChargeDialog.activeInHierarchy)
            {
                _View.BuyHasDiamondText.text = GameManager.ClientPlayer.Diamond.ToString();
            }
        }

        /// <summary>
        /// 赋值 数据统计UI界面数据
        /// </summary>
        private void SetSummaryUIData()
        {
            var player = GameManager.ClientPlayer;
            _View.S_HeadImage.sprite = GameManager.ClientPlayer.HeadSprite;
            _View.S_NameText.text = player.Name;
            _View.S_UserIDText.text = string.Format("ID:{0}", player.UserId.ToString());
            _View.RuChiShenglvText.text = CheckString(string.Format("{0}%", player.BaseDetailData.inbound_win_rate));
            _View.RuChiLvText.text = CheckString(string.Format("{0}%", player.BaseDetailData.inbound_rate));
            _View.ZongJuText.text = CheckString(player.BaseDetailData.total_game.ToString());
            _View.ZongShouText.text = CheckString(string.Format("{0}", player.BaseDetailData.total_hand));
            _View.SummaryScrollbar.value = 1f;
            _View.WinNumberText.text = string.Format("-");
            _View.MarryNumberText.text = string.Format("-");
            _View.ChangJunZhanJiText.text = string.Format("-");
            _View.ZhanJi02Text.text = string.Format("-");
            _View.PFRText.text = string.Format("-");
            _View.Bet_3Text.text = string.Format("-");
            _View.AFText.text = string.Format("-");
            _View.CBetText.text = string.Format("-");

            GameManager.ClientPlayer.MoreDetailData = null;

            //判断是否是银卡以上的用户
            //            _View.SpecialButton.SetActive(player.VIP == 0);
            //            if (player.VIP > 0)
            //            {
            //                OnSpecialButton(null);
            //            }
            _View.SpecialButton.SetActive(false);
            OnSpecialButton(null);
        }

        /// <summary>
        /// 设置更多数据显示--特权数据显示
        /// </summary>
        public void SetMoreDetailData()
        {
            var data = GameManager.ClientPlayer.MoreDetailData;
            _View.WinNumberText.text = CheckString(string.Format("{0}", data.win));
            _View.MarryNumberText.text = CheckString(string.Format("{0}", data.avg_carry));
            _View.ChangJunZhanJiText.text = CheckString(string.Format("{0}", data.avg_result));
            _View.ZhanJi02Text.text = CheckString(string.Format("{0}", data.result_pre100));
            _View.PFRText.text = CheckString(string.Format("{0}%", data.preflop_raise_rate));
            _View.Bet_3Text.text = CheckString(string.Format("{0}%", data.more_preflop_raise_rate));
            _View.AFText.text = CheckString(string.Format("{0}", data.af_rate));
            _View.CBetText.text = CheckString(string.Format("{0}%", data.continue_bet_Rate));
        }


        #region ------------------------- --SliderChange CallBack

        public void TimeSliderChange()
        {
            int value = (int)_View.TimeSlider.value;
            string msg = "";
            switch (value)
            {
                case 0:
                    _CurrPokerTime = 30;
                    msg = "30分";
                    break;
                case 1:
                    _CurrPokerTime = 60;
                    msg = "1小时";
                    break;
                case 2:
                    _CurrPokerTime = 90;
                    msg = "1.5小时";
                    break;
                case 3:
                    _CurrPokerTime = 120;
                    msg = "2小时";
                    break;
                case 4:
                    _CurrPokerTime = 150;
                    msg = "2.5小时";
                    break;
                case 5:
                    _CurrPokerTime = 240;
                    msg = "4小时";
                    break;
                case 6:
                    _CurrPokerTime = 360;
                    msg = "6小时";
                    break;
                default:
                    break;
            }
            _View.TimeSliderText.text = msg;
        }

        public void BlindSliderChange()
        {
            int value = (int)_View.BlindSlider.value;
            switch (value)
            {
                case 0:
                    {
                        _CurrSmallBlind = 1;
                    }
                    break;
                case 1:
                    {
                        _CurrSmallBlind = 5;
                    }
                    break;
                case 2:
                    {
                        _CurrSmallBlind = 10;
                    }
                    break;
                case 3:
                    {
                        _CurrSmallBlind = 20;
                    }
                    break;
                case 4:
                    {
                        _CurrSmallBlind = 25;
                    }
                    break;
                case 5:
                    {
                        _CurrSmallBlind = 50;
                    }
                    break;
                case 6:
                    {
                        _CurrSmallBlind = 100;
                    }
                    break;
                case 7:
                    {
                        _CurrSmallBlind = 200;
                    }
                    break;
                case 8:
                    {
                        _CurrSmallBlind = 300;
                    }
                    break;
                case 9:
                    {
                        _CurrSmallBlind = 500;
                    }
                    break;
                case 10:
                    {
                        _CurrSmallBlind = 1000;
                    }
                    break;
                default:
                    break;
            }
            _CurrBigBlind = _CurrSmallBlind * 2;
            _View.BlindText.text = string.Format("{0}/{1}", _CurrSmallBlind, _CurrBigBlind);
            MinCarrySliderChange();
            SetCreateRoomStatus();
        }

        public void PeoPleSliderChange()
        {
            int value = (int)_View.PeopleSlider.value;
            _CurrPeopleNumber = value + 2;
            _View.PeopleNumberText.text = string.Format("{0}人", _CurrPeopleNumber);
        }

        public void MinCarrySliderChange()
        {
            int value = (int)_View.MinCarrySlider.value + 1;
            _CurrMinCarry = (value * 50) * _CurrBigBlind;
            _View.MinCarryText.text = _CurrMinCarry.ToString();
            _View.SmallNumberText.text = _CurrMinCarry.ToString();
            MaxCarrySliderChagne();
        }

        public void MaxCarrySliderChagne()
        {
            int value = (int)_View.MaxCarrySlider.value + 1;
            _CurrMaxCarry = _CurrMinCarry * value * 10;
            _View.MaxCarryText.text = _CurrMaxCarry.ToString();
        }

        public void AnteSliderChange()
        {
            int value = (int)_View.AnteSlider.value;
            _CurrAnteNumber = value;
            _View.AnteText.text = _CurrAnteNumber.ToString();
        }

        public void TotalSliderChange()
        {
            int value = (int)_View.TotalCarrySlider.value;
            if (value == 21)
            {
                if (_View.CtrollerCarryToggle.isOn)
                {
                    _CurrTotalCarryNumber = -1;
                    _View.TotalCarryText.text = "无上限";
                }
                else
                {
                    _CurrTotalCarryNumber = 20 * 20000;
                    _View.TotalCarryText.text = _CurrTotalCarryNumber.ToString();
                }
            }
            else
            {
                _CurrTotalCarryNumber = value * 20000;
                _View.TotalCarryText.text = _CurrTotalCarryNumber.ToString();
            }
        }

        public void ToggleCtroller()
        {
            if (_View.CtrollerCarryToggle.isOn)
            {
                _CurrTotalCarryNumber = 0;
                _View.TotalCarrySlider.value = 0f;
            }
        }

        public void MyGetRoomToggle01()
        {
            if (_View.MyGetRoomToggle01.isOn)
            {
                ClientEvent_Hall.Instance.RequestGetSummaryLists(0);
            }
        }

        public void MyGetRoomToggle02()
        {
            if (_View.MyGetRoomToggle02.isOn)
            {
                ClientEvent_Hall.Instance.RequestGetSummaryLists(7);
            }
        }

        public void MyGetRoomToggle03()
        {
            if (_View.MyGetRoomToggle03.isOn)
            {
                ClientEvent_Hall.Instance.RequestGetSummaryLists(30);
            }
        }

        public void MyGetRoomToggle04()
        {
            if (_View.MyGetRoomToggle04.isOn)
            {
                ClientEvent_Hall.Instance.RequestGetSummaryLists(-1);
            }
        }

        #endregion

        #region ------------- 钱包

        private Dictionary<string, WalletItemInfo> _WalletItemInfoDics = new Dictionary<string, WalletItemInfo>();
        private EWalletType _CurrChooseWallType;

        public void SetWalletData(string respData)
        {
            var data = JsonUtility.FromJson<WalletListsRet>(respData);
            if (_WalletItemInfoDics != null)
            {
                _WalletItemInfoDics.Clear();
            }
            for (int i = 0; i < data.list.Length; i++)
            {
                if (!_WalletItemInfoDics.ContainsKey(data.list[i].currency_id))
                {
                    _WalletItemInfoDics.Add(data.list[i].currency_id, data.list[i]);
                }
            }
            //            _CurrChooseWallType = EWalletType.BTC;
            //            SetWalletPublicData();
            //            _View.WalletDialogView.CBObj.SetActive(false);
            //            _View.WalletDialogView.TBObj.SetActive(false);
            //            _View.WalletDialogView.GoumaiObj.SetActive(false);
            //            _View.WalletDialogView.MaiChuObj.SetActive(false);
            if (_View.WalletDialogView.TBObj.activeInHierarchy)
            {
                //                _CurrChooseWallType = EWalletType.BTC;
                SetTBDialogData();
            }
            else if (_View.WalletDialogView.GoumaiObj.activeInHierarchy)
            {
                SetGMDialogData();
            }
            else if (_View.WalletDialogView.MaiChuObj.activeInHierarchy)
            {
                SetMCDialogData();
            }
            else
            {
                _CurrChooseWallType = EWalletType.BTC;
            }
            SetWalletPublicData();
        }

        private void SetWalletPublicData()
        {
            _View.WalletDialogView.CurrGoldText.text = GameManager.ClientPlayer.Balance.ToString();
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                _View.WalletDialogView.CurrAmountText.text = _WalletItemInfoDics[_CurrChooseWallType.ToString()].amount.ToString();
            }
            else
            {
                _View.WalletDialogView.CurrAmountText.text = "0";
            }

            _View.WalletDialogView.WalletMoneyIcon.sprite = AtlasMapping.Instance.GetAtlas("Wallet", string.Format("icon_{0}", _CurrChooseWallType.ToString()));
            _View.WalletDialogView.WalletCBMoneyIcon.sprite = AtlasMapping.Instance.GetAtlas("Wallet", string.Format("icon_{0}", _CurrChooseWallType.ToString()));
            _View.WalletDialogView.WalletTBMoneyIcon.sprite = AtlasMapping.Instance.GetAtlas("Wallet", string.Format("icon_{0}", _CurrChooseWallType.ToString()));
            _View.WalletDialogView.WalletGMMoneyIcon.sprite = AtlasMapping.Instance.GetAtlas("Wallet", string.Format("icon_{0}", _CurrChooseWallType.ToString()));
            _View.WalletDialogView.WalletMCMoneyIcon.sprite = AtlasMapping.Instance.GetAtlas("Wallet", string.Format("icon_{0}", _CurrChooseWallType.ToString()));

        }

        /// <summary>
        /// 设置充币界面数据
        /// </summary>
        private void SetCBDailogData()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.CBAdressText.text = data.address;
                _View.WalletDialogView.CBAdressTipsText.text = string.Format("{0}钱包地址：", _CurrChooseWallType.ToString());
                _View.WalletDialogView.CBTypeText.text = _CurrChooseWallType.ToString();
                _View.WalletDialogView.CBTipsText.text = string.Format("请使用{0}钱包扫码或填入付款地址来完成充币，{0}钱包地址禁止充值除{0}之外的其它资产，任何非{0}充币将不可找回；充币成功后，需要兑换筹码方可进行游戏。", data.currency_id);
                StartCoroutine(GetWalletImage(data.qr));
            }
        }

        IEnumerator GetWalletImage(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.texture != null)
            {
                var sprite = PokerUtility.GetTexture2DToSprite(www.texture);
                _View.WalletDialogView.CBCodeImage.sprite = sprite;
                www.Dispose();
            }
            StopCoroutine("GetWalletImage");
        }

        private void SetTBDialogData()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.TBCurrMoneyText.text = data.amount.ToString();
                _View.WalletDialogView.TBTipsmoneyText.text = string.Format("{0} {1}", data.fee, _CurrChooseWallType.ToString());
                _View.WalletDialogView.TBDescText.text = string.Format("{0}钱包只能向{0}地址发送资产，如果向非{0}地址发送资产将不可找回；矿工费将在可用余额中扣除 新用户提币限制说明请查看公告", data.currency_id);
                _View.WalletDialogView.TBNumberInputField.text = "";
                _View.WalletDialogView.TBReciveAdressInputField.text = "";
                if (data.has_qr)
                {
                    _View.WalletDialogView.TBPlaceholderText.text = string.Format("您当前最多可提取资产{0}", data.amount.ToFloat() - data.fee.ToFloat());
                }
                else
                {
                    _View.WalletDialogView.TBPlaceholderText.text = string.Format("您当前最多可提取资产{0}", 0);
                }
            }
        }

        /// <summary>
        /// 设置购买金币界面数据
        /// </summary>
        private void SetGMDialogData()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.GMCurrAmountText.text = data.amount.ToString();
                _View.WalletDialogView.GMCurrGoldText.text = GameManager.ClientPlayer.Balance.ToString();
                //                _View.WalletDialogView.GMHuiLvText.text = "-";
                _View.WalletDialogView.GMNumberInputField.text = "";
                _View.WalletDialogView.GMGetChipsNumberText.text = "0";
                _View.WalletDialogView.GMHuiLvText.text = string.Format("1{0}={1}筹码", _CurrChooseWallType.ToString(), data.buy_rate);
            }
        }

        private void SetMCDialogData()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.MCCurrAmountText.text = data.amount.ToString();
                _View.WalletDialogView.MCCurrGoldText.text = GameManager.ClientPlayer.Balance.ToString();
                //                _View.WalletDialogView.MCHuiLvText.text = "-";
                _View.WalletDialogView.MCNumberInputField.text = "";
                _View.WalletDialogView.MCGetChipsNumberText.text = "0";
                _View.WalletDialogView.MCHuiLvText.text = string.Format("1{0}={1}筹码", _CurrChooseWallType.ToString(), data.sell_rate);
            }
        }

        public void ChangeGMInputField()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                //                Debug.LogError(_View.WalletDialogView.GMNumberInputField.text);
                //                if (IsNumeric(_View.WalletDialogView.GMNumberInputField.text))
                {
                    _View.WalletDialogView.GMGetChipsNumberText.text = string.Format("{0}", UnityEngine.Mathf.FloorToInt(_View.WalletDialogView.GMNumberInputField.text.ToFloat() * data.buy_rate.ToFloat()));
                }
            }
        }

        public void ChangeMCInputField()
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var data = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                //                if (IsNumeric(_View.WalletDialogView.MCNumberInputField.text))
                {
                    _View.WalletDialogView.MCGetChipsNumberText.text = string.Format("{0}", (_View.WalletDialogView.MCNumberInputField.text.ToFloat() / data.sell_rate.ToFloat()));
                }
            }
        }

        //        public static bool IsNumeric(string value)
        //        {
        //            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        //        }

        #endregion


        #region -------Button click event

        private void OnGongGaoButtonClicked(EventData data)
        {
            ChangeTableSprite();
            if (data != null)
            {
                data.Get<GameObject>(0).Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "nav_icon_news_mouseover@2x");
            }
            else
            {
                _View.GongGaoButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "nav_icon_news_mouseover@2x");
            }
            ShowDialog(_View.GongGaoDialog);
            if (_CurrNotes == "")
            {
                ClientEvent_Hall.Instance.RequestGetNotesLists();
            }
            else
            {
                InitNotesListsGridScroller();
            }
        }

        private void OnYueJuButtonClicked(EventData data)
        {
            ChangeTableSprite();
            data.Get<GameObject>(0).Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_pk_mouseover@2x");
            ShowDialog(_View.YueJu_KuaiSuDialog);
            _View.YueJu_CreateMatchDialog.SetActive(false);
        }

        private void OnFaXianButtonClicked(EventData data)
        {
            ChangeTableSprite();
            ShowDialog(_View.FX_MidleDialog);
            if (data != null)
            {
                OnFX_GeRenButton(null);
                data.Get<GameObject>(0).Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_find_mousefouse@2x");
            }
            else
            {
                _View.FaXianButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_find_mousefouse@2x");
            }
        }

        private void OnJuLeBuButtonClciked(EventData data)
        {
            ChangeTableSprite();
            if (data != null)
            {
                data.Get<GameObject>(0).Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_club_mousefouse@2x");
            }
            else
            {
                _View.JuLeBuButton.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "icon_tab_club_mousefouse@2x");
            }
            ShowDialog(null);
            ClientEvent_Hall.Instance.RequestGetMyClubLists();
        }

        private void OnMeButtonClciked(EventData data)
        {
            ChangeTableSprite();
            data.Get<GameObject>(0).Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Other", "nav_icon_itsme_mouseover@2x");
            ShowDialog(_View.Me_MainDialog);

            ClientEvent_Hall.Instance.RequestGetBaseDetail();
            ClientEvent_Hall.Instance.RequestUserInfo();
            ClientEvent_Hall.Instance.RequestGetMessage();

            StartCoroutine(UpdateUserInfo());
        }

        private void OnYJ_JoinMatchButtonClicked(EventData data)
        {
            string msg = _View.YJ_InputTextField.text;
            if (msg != "")
            {
                int id;
                if (int.TryParse(msg, out id))
                {
                    GameManager.ClientPlayer.RoomInviteId = id;
                    ClientEvent_Hall.Instance.RequestInviteJoinRoom(id, 0);
                }
            }
            else
            {
                "邀请码不能为空".ShowWarningTextTips();
            }
        }

        /// <summary>
        /// 创建房间牌局按钮
        /// </summary>
        /// <param name="data">Data.</param>
        private void OnYJ_CreateMatchButtonClicked(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.YueJu_CreateMatchDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            //设置初始化创建房间的数据
            //            if (_CurrClubOpType == ClubOpType.CreateClubMatch)
            //            {
            //                _View.InputFieldRoomName.text = string.Format("CLUB-{0}{1}.{2}{3}{4}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //            }
            //            else
            //            {
            //                _View.InputFieldRoomName.text = string.Format("PT-{0}{1}.{2}{3}{4}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //            }
            _View.InputFieldRoomName.text = string.Format("{0}", DateTime.Now.ToString("MMdd-HHmmss"));
            if (_IsFirstOpen)
            {
                _View.BlindSlider.value = 2f;
                _CurrSmallBlind = 10;
                _CurrBigBlind = 20;
                _View.TimeSlider.value = 0f;
                _View.PeopleSlider.value = 7f;
                _CurrPeopleNumber = 9;
                _View.MinCarrySlider.value = 1f;
                _CurrMinCarry = 2000;
                _View.MaxCarrySlider.value = 0f;
                _CurrMaxCarry = 20000;
                _View.AnteSlider.value = 0f;
                _View.TotalCarrySlider.value = (float)_CurrSmallBlind * 10000f / 20000f;
                _CurrTotalCarryNumber = _CurrSmallBlind * 10000;

                _View.InsuranceToggle.isOn = false;
                _View.CtrollerCarryToggle.isOn = false;
                _View.StraddleToggle.isOn = false;
                _View.AutoPokerToggle.isOn = false;
                _View.TwoSevenToggle.isOn = false;
                _View.IPToggle.isOn = false;
                _View.GPSToggle.isOn = false;
                _View.ClubTeamToggle.isOn = false;

                _IsFirstOpen = false;
            }
            _View.MyGoldText.text = string.Format("个人财富：{0}", GameManager.ClientPlayer.Balance);
            SetCreateRoomStatus();
        }

        private void OnJLB_JoinClubButtonClciked(EventData data)
        {
            _View.CB_AddClubDialog.SetActive(false);
            GameManager.Instance.SetTweenAnimation(_View.CB_JoinClubDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnJLB_CreateClubButtonClciked(EventData data)
        {
            if (GameManager.ClientPlayer.ClubRemainNumber > 0)
            {
                _View.CB_AddClubDialog.SetActive(false);
                GameManager.Instance.SetTweenAnimation(_View.CB_CreateClubDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);

                Const.Province = "广东省";
                Const.City = "广州市";

                _View.CreateClubNumberText.text = GameManager.ClientPlayer.ClubRemainNumber.ToString();
            }
            else
            {
                GameManager.Instance.SetTweenAnimation(_View.CB_DontCreateClubDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
        }

        private void OnCM_HightSettingButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CM_HightSettingDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnCM_OkButton(EventData data)
        {
            if (_View.InputFieldRoomName.text != "")
            {
                Dictionary<string, object> req = new Dictionary<string, object>();
                req.Add("alliance", _IsCreatePokerTeam);
                req.Add("ante", _CurrAnteNumber);//地注
                req.Add("big_blind", _CurrBigBlind);//大盲
                req.Add("club_id", 0);//俱乐部ID
                req.Add("expire", _CurrPokerTime);//时间 分钟单位
                req.Add("gps", _View.GPSToggle.isOn);//gps限制
                req.Add("insurance", _View.InsuranceToggle.isOn);//购买保险
                req.Add("ip", _View.IPToggle.isOn);//ip限制
                req.Add("max", _CurrPeopleNumber);//人数
                req.Add("max_carry", _CurrMaxCarry);
                req.Add("min_carry", _CurrMinCarry);
                req.Add("name", _View.InputFieldRoomName.text);//房间名称
                req.Add("rule_27", _View.TwoSevenToggle.isOn);//27玩法
                req.Add("small_blind", _CurrSmallBlind);
                req.Add("straddle", _View.StraddleToggle.isOn);//未知玩法
                req.Add("total_carry", _CurrTotalCarryNumber);//总带入
                req.Add("ctrl_carry", _View.CtrollerCarryToggle.isOn);//控制带入
                req.Add("auto_bury", _View.AutoPokerToggle.isOn);//自动埋牌
                //当前状态是创建俱乐部的操作，并且 _CurrChooseClubListsItemData 不等于空
                if (_CurrClubOpType == ClubOpType.CreateClubMatch && _CurrChooseClubListsItemData != null)
                {
                    req["club_id"] = _CurrChooseClubListsItemData.id;
                    //                    req["alliance"] = _View.ClubTeamToggle.isOn;
                    ClientEvent_Hall.Instance.RequestCreateRoom(req, 1);
                }
                else
                {
                    ClientEvent_Hall.Instance.RequestCreateRoom(req, 1);
                }
            }
            else
            {
                "房间名不能为空".ShowWarningTextTips();
            }

        }

        private void OnCM_BackButton(EventData data)
        {
            _IsCreatePokerTeam = false;
            if (_CurrClubOpType == ClubOpType.CreateClubMatch)
            {
                OnJuLeBuButtonClciked(null);
            }
            GameManager.Instance.SetTweenAnimation(_View.YueJu_CreateMatchDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnCM_HightSettingOkButton(EventData data)
        {
            SetCreateRoomStatus();
            GameManager.Instance.SetTweenAnimation(_View.CM_HightSettingDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnCM_HightSettingHelpButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CM_HightSettingTipsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnCM_HightSettingTipsBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CM_HightSettingTipsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnCM_HightSettingBackButton(EventData data)
        {
            SetCreateRoomStatus();
            GameManager.Instance.SetTweenAnimation(_View.CM_HightSettingDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        //        private void OnFX_SmallButton(EventData data)
        //        {
        //            _View.FX_SmallDialog.SetActive(false);
        //            _View.FX_MaxDialog.SetActive(true);
        //        }
        //
        //        private void OnFX_MaxButton(EventData data)
        //        {
        //            _View.FX_SmallDialog.SetActive(true);
        //            _View.FX_MaxDialog.SetActive(false);
        //        }

        private void OnCB_CreateClubOkButton(EventData data)
        {
            if (GameManager.ClientPlayer.ClubRemainNumber > 0)
            {
                if (_View.CB_CreateInputField.text != "")
                {
                    ClientEvent_Hall.Instance.RequestCreateClub(_View.CreateClubAreaText.text, "俱乐部头像暂无", _View.CB_CreateInputField.text, "特殊的俱乐部！！！");
                }
                else
                {
                    "俱乐部名称不能为空".ShowWarningTextTips();
                }
            }
            else
            {
                GameManager.Instance.SetTweenAnimation(_View.CB_DontCreateClubDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
        }

        private void OnCB_CreateClubBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CB_CreateClubDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnCB_AddClubCloseButton(EventData data)
        {
            _View.CB_AddClubDialog.SetActive(false);
        }

        private void OnCB_CreateListsAddClubButton(EventData data)
        {
            _View.CB_AddClubDialog.SetActive(true);
        }

        private void OnCB_DCCBackButton(EventData data)
        {
            OnJuLeBuButtonClciked(null);
        }

        private void OnCB_DCCBuyVIPButton(EventData data)
        {
            OnMe_ShopButton(null);
            Invoke("ChangeShopScrollbar", 0.5f);
        }

        private void OnCB_JoinClubBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CB_JoinClubDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnCB_JoinClubOkButton(EventData data)
        {
            if (_View.CB_SearchInputField.text != "")
            {
                ClientEvent_Hall.Instance.RequestSearchClub(_View.CB_SearchInputField.text);
            }
            else
            {
                "俱乐部名称或ID不能为空".ShowWarningTextTips();
            }
        }

        private void OnRoomlistsItemClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIRoomListsItemView>();
            GameManager.ClientPlayer.RoomInviteId = view.Data.Invitecode;
            //            if (view.Data.Create_type == 1)
            //            {
            //                //加入俱乐部房间
            //                ClientEvent_Hall.Instance.RequestClubRoom(view.Data.Id, view.Data.CircleId);
            //            }
            //            else if (view.Data.CircleId == -1)
            //            {
            ////                if (_CurrChooseClubListsItemData.leve < 50)
            ////                {
            ////                    "您的俱乐部权限不足，请俱乐部管理员联系客服提升权限".ShowWarningTextTips();
            ////                    return;
            ////                }
            //                GameManager.ClientPlayer.CurrChooseClubId = _CurrChooseClubListsItemData.id;
            //                //战队房间
            //                ClientEvent_Hall.Instance.RequestClubRoom(view.Data.Id, _CurrChooseClubListsItemData.id);
            //            }
            //            else
            //            {
            //                //加入普通房间
            //                ClientEvent_Hall.Instance.RequestInviteJoinRoom(view.Data.Invitecode, view.Data.Id);
            //            }
            ClientEvent_Hall.Instance.RequestInviteJoinRoom(view.Data.Invitecode, view.Data.Id);
        }

        private void OnMe_ShopButton(EventData data)
        {
            //            ClientEvent_Hall.Instance.RequestGetGoodLists();
            //            "充值钻石请查看公告联系客服".ShowWarningTextTips();

            RequestWallets();
        }

        /// <summary>
        /// 获取钱包列表
        /// </summary>
        public void RequestWallets()
        {
            if (!_View.WalletDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.WalletDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);

                _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "BTC");
                _View.WalletDialogView.CBObj.SetActive(false);
                _View.WalletDialogView.TBObj.SetActive(false);
                _View.WalletDialogView.GoumaiObj.SetActive(false);
                _View.WalletDialogView.MaiChuObj.SetActive(false);
            }
            ClientEvent_Hall.Instance.RequestWallets();
        }

        private void OnMe_HistoryButton(EventData data)
        {
            if (!_View.HistoryDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.HistoryDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            ClientEvent_Hall.Instance.RequestGetSummaryHistroy(7);
        }

        private void OnAllHistoryButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestGetSummaryHistroy(-1);
        }

        private void OnMe_SettingButton(EventData data)
        {
            _View.SoundTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", Const.IsSound ? "swith_btn_close@2x" : "swith_btn_open@2x");
            _View.VibrationTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", Const.IsVibrate ? "swith_btn_close@2x" : "swith_btn_open@2x");
            GameManager.Instance.SetTweenAnimation(_View.SettingDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnMe_SummaryButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.Me_SummaryDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            SetSummaryUIData();
        }

        private void OnAnalysisButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.AnalysisDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);

            var player = GameManager.ClientPlayer;
            _View.AnalysisHeadImage.sprite = GameManager.ClientPlayer.HeadSprite;
            _View.AnalysisNameText.text = player.Name;
            _View.AnalysisIdText.text = string.Format("ID:{0}", player.UserId.ToString());
            _View.AnalysisVPIPText.text = CheckString(string.Format("{0}%", player.BaseDetailData.inbound_rate));
            _View.AnalysisScrollbar.value = 1f;
            if (player.MoreDetailData != null)
            {
                _View.AnalysisPFRText.text = CheckString(string.Format("{0}%", player.MoreDetailData.preflop_raise_rate));
                _View.AnalysisAFText.text = CheckString(string.Format("{0}", player.MoreDetailData.af_rate));
                _View.AnalysisBetText.text = CheckString(string.Format("{0}%", player.MoreDetailData.more_preflop_raise_rate));
                _View.AnalysisCBetText.text = CheckString(string.Format("{0}%", player.MoreDetailData.continue_bet_Rate));
            }
            else
            {
                _View.AnalysisPFRText.text = string.Format("-");
                _View.AnalysisAFText.text = string.Format("-");
                _View.AnalysisBetText.text = string.Format("-");
                _View.AnalysisCBetText.text = string.Format("-");
            }
        }

        private string CheckString(string msg)
        {
            string value = msg;
            if (msg == "0%" || msg == "0" || msg == "")
            {
                value = "-";
            }
            return value;
        }

        private void OnSpecialButton(EventData data)
        {
            if (GameManager.ClientPlayer.Balance > 30 || GameManager.ClientPlayer.VIP > 0)
            {
                _View.SpecialButton.SetActive(false);
                ClientEvent_Hall.Instance.RequestGetMoreDetail();
                GameManager.ClientPlayer.Balance -= 30;
            }
            else
            {
                "筹码不足，请前往我的钱包兑换".ShowWarningTextTips();
            }
        }

        private void OnBuyLevelCardButton(EventData data)
        {
            OnMe_ShopButton(null);
            Invoke("ChangeShopScrollbar", 0.5f);
        }

        private void ChangeShopScrollbar()
        {
            _View.Scrollbar_Shop.value = 0f;
        }

        private void OnS_BackButton(EventData data)
        {
            GameManager.ClientPlayer.MoreDetailData = null;
            GameManager.Instance.SetTweenAnimation(_View.Me_SummaryDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnSettingBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.SettingDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnShopGiftBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ShopGiftDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnChargeBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ChargeDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnHistoryBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.HistoryDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnShopGiftToChargeButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestGetChargeGoodLists();
        }

        private void OnLogoutButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestLogout();
        }

        private void OnClublistsItemClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIClubListItemView>();
            _CurrChooseClubListsItemData = view.ClubListItemData;

            if (_CurrChooseClubListsItemData.avatar.StartsWith("http"))
            {
                UIHeadImageData headData = new UIHeadImageData();
                headData.Avatar = _CurrChooseClubListsItemData.avatar;
                headData.HeadImage = _View.MyClubHeadImageButton;
                StartCoroutine(GetClubHeadSprite(headData));
            }
            else
            {
                _View.MyClubHeadImageButton.sprite = AtlasMapping.Instance.GetAtlas("Other", "img_club@2x");
            }

            if (_CurrClubOpType == ClubOpType.Serach)
            {
                GameManager.Instance.SetTweenAnimation(_View.CB_OtherClubMessageDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                _View.Other_ClubNameText.text = _CurrChooseClubListsItemData.name;
                _View.Other_IdText.text = string.Format("ID: {0}", _CurrChooseClubListsItemData.id);
                _View.Other_PeopleNumberText.text = string.Format("{0}/{1}", _CurrChooseClubListsItemData.member_total + 1, _CurrChooseClubListsItemData.max_member_total);
                _View.Other_AreaText.text = string.Format("{0}", _CurrChooseClubListsItemData.area_code);
                _View.Other_CreateTimeText.text = string.Format("创建于{0}", _CurrChooseClubListsItemData.created);
                _View.Other_DescText.text = string.Format("{0}", _CurrChooseClubListsItemData.remark);
                _View.Other_CreateUserNameText.text = string.Format("{0}", _CurrChooseClubListsItemData.create_user.nick_name);
                _View.Other_HeadIamge.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetHeadSpriteName(_CurrChooseClubListsItemData.create_user.gender));
            }
            else if (_CurrClubOpType == ClubOpType.GetMyClubList)
            {
                _IsChangeArea = false;
                GameManager.Instance.SetTweenAnimation(_View.CB_MyClubMessageDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                _View.EditorClub_Name.text = _CurrChooseClubListsItemData.name;
                _View.My_IdText.text = string.Format("ID: {0}", _CurrChooseClubListsItemData.id);
                _View.My_PeopleNumberText.text = string.Format("{0}/{1}", _CurrChooseClubListsItemData.member_total + 1, _CurrChooseClubListsItemData.max_member_total);
                _View.My_AreaText.text = string.Format("{0}", _CurrChooseClubListsItemData.area_code);
                _View.My_CreateTimeText.text = string.Format("创建于{0}", _CurrChooseClubListsItemData.created);
                _View.EditorClub_Remark.text = string.Format("{0}", _CurrChooseClubListsItemData.remark);

                _View.EditorClub_CreateUserNameText.text = _CurrChooseClubListsItemData.create_user.nick_name;
                _View.EditorClub_LevelText.text = _CurrChooseClubListsItemData.leve.ToString();
                _View.EditorClub_FundNumText.text = _CurrChooseClubListsItemData.fund.ToString();
                //                _View.XiaoXiToogle.isOn = _CurrChooseClubListsItemData.open_message;
                //                _View.OpenToggle.isOn = _CurrChooseClubListsItemData.open_create_limit;

                if (_CurrChooseClubListsItemData.leve < 50)
                {
                    _View.EditorClub_LevelText.text = "普通俱乐部";
                }
                else if (_CurrChooseClubListsItemData.leve >= 50 && _CurrChooseClubListsItemData.leve < 90)
                {
                    _View.EditorClub_LevelText.text = "联盟俱乐部";
                }
                else if (_CurrChooseClubListsItemData.leve >= 90)
                {
                    _View.EditorClub_LevelText.text = "官方俱乐部";
                }

                if (_CurrChooseClubListsItemData.create_user.id == GameManager.ClientPlayer.UserId)
                {
                    //俱乐部创建者
                    _View.My_DissolveButton.SetActive(true);
                    _View.My_SaveButton.SetActive(true);
                    _View.EditorClub_ExitButton.SetActive(false);
                    _View.EditorClub_Name.interactable = true;
                    _View.EditorClub_Remark.interactable = true;
                    _View.EditorClub_AreaButton.SetActive(true);
                    //                    _View.XiaoXiToogle.interactable = true;
                    //                    _View.OpenToggle.interactable = true;
                }
                else
                {
                    //非创建者
                    _View.My_DissolveButton.SetActive(false);
                    _View.My_SaveButton.SetActive(false);
                    _View.EditorClub_ExitButton.SetActive(true);
                    _View.EditorClub_Name.interactable = false;
                    _View.EditorClub_Remark.interactable = false;
                    _View.EditorClub_AreaButton.SetActive(false);
                    //                    _View.XiaoXiToogle.interactable = false;
                    //                    _View.OpenToggle.interactable = false;
                }
            }
        }

        private void OnOther_JoinButton(EventData data)
        {
            if (_CurrChooseClubListsItemData != null)
            {
                ClientEvent_Hall.Instance.RequestJoinClub(_CurrChooseClubListsItemData.id);
            }
        }

        private void OnOther_BackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.CB_OtherClubMessageDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnMy_BackButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestGetMyClubLists();
            GameManager.Instance.SetTweenAnimation(_View.CB_MyClubMessageDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnOMy_SaveButton(EventData data)
        {
            if (_CurrChooseHeadData != "")
            {
                ClientEvent_Hall.Instance.RequestClubUploadAvatar(_CurrChooseHeadData, _CurrChooseClubListsItemData.id);
            }
            ClientEvent_Hall.Instance.RequestUpdateClubInfo(_View.My_AreaText.text, "", _View.EditorClub_Name.text, false, true, _View.EditorClub_Remark.text, _CurrChooseClubListsItemData.id);
        }

        public void OnMy_GetApplyClubPeopleButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestGetMessage();
        }

        private void OnMy_DissolveButton(EventData data)
        {
            _View.PublicTipsDialog.SetActive(true);
            _CurrPublicOpType = PublicOpType.DissolutionClub;
            _View.PublicTipsText.text = string.Format("是否要解散该俱乐部？");
        }

        private void OnEditorClub_AreaButton(EventData data)
        {
            _IsChangeArea = true;
            GameManager.UI.OpenUIForm(UIFormId.UIChooseAreaPanel);
        }

        private void OnEditorClub_ExitButton(EventData data)
        {
            _View.PublicTipsDialog.SetActive(true);
            _CurrPublicOpType = PublicOpType.ExitClub;
            _View.PublicTipsText.text = string.Format("是否要退出该俱乐部？");
        }

        public void OnPublicOkButton(EventData data)
        {
            if (_CurrPublicOpType == PublicOpType.DissolutionClub)
            {
                ClientEvent_Hall.Instance.RequestDissolution(_CurrChooseClubListsItemData.id);
            }
            else if (_CurrPublicOpType == PublicOpType.DeleteClubPeople)
            {
                ClientEvent_Hall.Instance.RequestDeleteClubPeople(_CurrDeleteMemberUserId, _CurrChooseClubListsItemData.id);
            }
            else if (_CurrPublicOpType == PublicOpType.ExitClub)
            {
                ClientEvent_Hall.Instance.RequestExitClub(_CurrChooseClubListsItemData.id);
                OnJuLeBuButtonClciked(null);
            }
            OnPublicCancelButton(null);
        }

        public void OnPublicCancelButton(EventData data)
        {
            _View.PublicTipsDialog.SetActive(false);
        }

        private void OnItemCreateButton(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIClubListItemView>();
            _CurrChooseClubListsItemData = view.ClubListItemData;

            if (view.ClubListItemData.room_status == -1)
            {
                GameManager.Instance.SetTweenAnimation(_View.FX_ClubPokerDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                //                _View.MePokerButton.SetActive(true);
                //                _View.AllPokerButton.SetActive(false);
                ClientEvent_Hall.Instance.RequestGetClubRooms(view.ClubListItemData.id);
            }
            else
            {
                if (_CurrChooseClubListsItemData.is_manager)
                {
                    OnCarryOpButton(null);
                }
                else
                {
                    "只有管理员可以进入".ShowWarningTextTips();
                }
            }

            //            return;

            //            if (_CurrClubOpType == ClubOpType.GetMyClubList)
            //            {
            //                if (view.ClubListItemData.room_status != 0)
            //                {
            //                    GameManager.Instance.SetTweenAnimation(_View.FX_ClubPokerDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            //                    ClientEvent_Hall.Instance.RequestGetClubRooms(view.ClubListItemData.id);
            //                    return;
            //                }
            //
            //                if (!_CurrChooseClubListsItemData.open_create_limit || _CurrChooseClubListsItemData.is_manager || _CurrChooseClubListsItemData.create_user.id == GameManager.ClientPlayer.UserId)
            //                {
            //                    if (view.ClubListItemData.room_status == 0)
            //                    {
            //                        _View.CB_MyClubListsDialog.SetActive(false);
            //                        _CurrClubOpType = ClubOpType.CreateClubMatch;
            //                        OnYJ_CreateMatchButtonClicked(null);
            //                    }
            //                }
            //                else
            //                {
            //                    "权限不足".ShowWarningTextTips();
            //                    ClientEvent_Hall.Instance.RequestGetMyClubLists();
            //                }
            //            }
        }

        private void OnClubOpenPokerButton(EventData data)
        {
            if (_CurrChooseClubListsItemData.is_manager)
            {
                if (_CurrChooseClubListsItemData.leve >= 90)
                {
                    _View.ClubManagerDialog.SetActive(false);
                    _View.CB_MyClubListsDialog.SetActive(false);
                    _View.CB_MyClubMessageDialog.SetActive(false);
                    _IsCreatePokerTeam = true;
                    _CurrClubOpType = ClubOpType.CreateClubMatch;
                    OnYJ_CreateMatchButtonClicked(null);
                }
                else
                {
                    "俱乐部权限不足，请查看公告联系客服提升权限".ShowWarningTextTips();
                }
            }
            else
            {
                "权限不足".ShowWarningTextTips();
            }
        }

        private void OnMyClubListsBackButton(EventData data)
        {
            if (_CurrClubOpType == ClubOpType.Serach)
            {
                _View.CB_WeiJiaRuDialog.SetActive(true);
                _View.CB_MyClubListsDialog.SetActive(false);
            }
        }

        private void OnClubPeoplelistsItemClicked(EventData data)
        {
            //            var view = data.Get<GameObject>(0).Get<UIClubPeopleListItemView>();
            //            int num = UnityEngine.Random.Range(1000, 10000);
            //            ClientEvent_Hall.Instance.RequestFundIssue(num, _CurrChooseClubListsItemData.id, view.MemberInfo.id);
            //            string.Format("基金 -{0}", num).ShowWarningTextTips();
        }

        private void OnClubPeoplelistsItemUpButtonClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIClubPeopleListItemView>();
            if (GameManager.ClientPlayer.CurrClubJob == 1)
            {
                ClientEvent_Hall.Instance.RequestChangeClubPeoplePermissions(true, view.MemberInfo.id, _CurrChooseClubListsItemData.id);
            }
            else
            {
                "权限不够".ShowWarningTextTips();
            }
        }

        private void OnClubPeoplelistsItemDownButtonClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIClubPeopleListItemView>();
            if (GameManager.ClientPlayer.CurrClubJob == 1)
            {
                ClientEvent_Hall.Instance.RequestChangeClubPeoplePermissions(false, view.MemberInfo.id, _CurrChooseClubListsItemData.id);
            }
            else
            {
                "权限不够".ShowWarningTextTips();
            }
        }

        private void OnClubPeoplelistsItemDeleteButtonClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIClubPeopleListItemView>();
            if (GameManager.ClientPlayer.CurrClubJob < 3)
            {
                _CurrDeleteMemberUserId = view.MemberInfo.id;
                _CurrPublicOpType = PublicOpType.DeleteClubPeople;
                _View.PublicTipsDialog.SetActive(true);
                _View.PublicTipsText.text = string.Format("是否要删除成员 {0} ？", view.MemberInfo.nick_name);
            }
            else
            {
                "管理员才有权限删除成员".ShowWarningTextTips();
            }
        }

        private UIClubPeopleListItemView _CurrChooseUIClubPeopleListItemView;

        private void OnGiveFundButton(EventData data)
        {
            if (GameManager.ClientPlayer.CurrClubJob == 1)
            {
                var view = data.Get<GameObject>(0).Get<UIClubPeopleListItemView>();
                _CurrChooseUIClubPeopleListItemView = view;
                _View.GiveFundInputField.text = "";
                _View.GiveFundPlayerNameText.text = view.MemberInfo.nick_name;
                _View.GiveFundPlayeHeadImage.sprite = view.HeadImage.sprite;
                _View.GiveFundNumberText.text = _CurrChooseClubListsItemData.fund.ToString();
                GameManager.Instance.SetTweenAnimation(_View.GiveFundDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            else
            {
                "权限不够".ShowWarningTextTips();
            }
        }

        public void OnMyClubPeopleButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.MyClubPeopleListsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            ClientEvent_Hall.Instance.RequestGetClubDetail(_CurrChooseClubListsItemData.id);
        }

        private void OnMyClubPeopleListsBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.MyClubPeopleListsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnApplyClubPeopleOkClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIApplyPeopleItemView>();
            ClientEvent_Hall.Instance.RequestReplyApplyClubPeople(view.MemberInfo.club_id, view.MemberInfo.player.id);
        }

        private void OnApplyClubPeopleCancelClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIApplyPeopleItemView>();
            var lists = new List<int>();
            lists.Add(view.MemberInfo.player.id);
            ClientEvent_Hall.Instance.RequestDeleteApplyClubPeople(lists, view.MemberInfo.club_id);
        }

        private void OnApplyClubPeopleListsBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ApplyClubPeopleListsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnChooseAreaButton(EventData data)
        {
            GameManager.UI.OpenUIForm(UIFormId.UIChooseAreaPanel);
        }

        private void OnRuchiLvTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(2, new Vector3(-8.6f, 485.34f, 0));
        }

        private void OnRuchiLvTipsButtonUp(GameObject go, PointerEventData data)
        {
            _View.TextTipsDialog.SetActive(false);
        }

        private void OnRuchiShengLvTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(3, new Vector3(-8.6f, 358.17f, 0));
        }

        private void OnPFRTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(4, new Vector3(-8.6f, -251.71f, 0));
        }

        private void OnBetTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(5, new Vector3(-8.6f, -251.71f, 0));
        }

        private void OnAFTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(6, new Vector3(-8.6f, -402f, 0));
        }

        private void OnCBetTipsButtonDown(GameObject go, PointerEventData data)
        {
            ShowTextTipsDialog(7, new Vector3(-8.6f, -402f, 0));
        }

        private void ShowTextTipsDialog(int tableId, Vector3 pos)
        {
            _View.TextTipsDialog.SetActive(true);
            _View.TextTipsDialog.transform.localPosition = pos;
            IDataTable<DRTipsText> lists = GameManager.DataTable.GetDataTable<DRTipsText>();
            string msg = lists.GetDataRow(tableId).Desc;
            _View.TextTips.text = msg.ToReplaceSpace();
        }

        private void OnAnalysisBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.AnalysisDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnBuuyOkClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIShopGoodItemView>();
            if (GameManager.ClientPlayer.Diamond >= view.Data.price)
            {
                ClientEvent_Hall.Instance.RequestExchangeGoods(view.Data.id);
            }
            else
            {
                "钻石不足".ShowWarningTextTips();
            }
        }

        private void OnMe_MessageButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ApplyClubPeopleListsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            ClientEvent_Hall.Instance.RequestGetMessage();
        }

        private void OnAboundMeButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.AbountWeDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnChangePasswordButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ForgotDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnCleanButton(EventData data)
        {
            "清理缓存成功！".ShowWarningTextTips();
        }

        private void OnSoundButton(EventData data)
        {
            if (!Const.IsSound)
            {
                Const.IsSound = true;
                _View.SoundTipsObj.transform.localPosition = new Vector3(25, 0, 0);
                _View.SoundTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", "swith_btn_close@2x");
            }
            else
            {
                Const.IsSound = false;
                _View.SoundTipsObj.transform.localPosition = new Vector3(-25, 0, 0);
                _View.SoundTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", "swith_btn_open@2x");
            }
        }

        private void OnVibrationButton(EventData data)
        {
            if (!Const.IsVibrate)
            {
                Const.IsVibrate = true;
                _View.VibrationTipsObj.transform.localPosition = new Vector3(25, 0, 0);
                _View.VibrationTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", "swith_btn_close@2x");
            }
            else
            {
                Const.IsVibrate = false;
                _View.VibrationTipsObj.transform.localPosition = new Vector3(-25, 0, 0);
                _View.VibrationTipsObj.Get<Image>().sprite = AtlasMapping.Instance.GetAtlas("Login", "swith_btn_open@2x");
            }
        }

        private void OnForgotOkButton(EventData data)
        {
            if (_View.ForgotInputMobileNumber.text == "")
            {
                "手机号不能为空".ShowWarningTextTips();
                return;
            }
            if (_View.ForgotInputCode.text == "")
            {
                "验证码不能为空".ShowWarningTextTips();
                return;
            }
            if (_View.ForgotInputNewPassword.text == "")
            {
                "新密码不能为空".ShowWarningTextTips();
                return;
            }
            if (_View.ForgotInputNewPasswordAgain.text == "")
            {
                "确认密码不能为空".ShowWarningTextTips();
                return;
            }

            if (_View.ForgotInputNewPassword.text != _View.ForgotInputNewPasswordAgain.text)
            {
                _View.ForgotTips.SetActive(_View.ForgotInputNewPassword.text != _View.ForgotInputNewPasswordAgain.text);
            }
            else
            {
                ClientEvent_Hall.Instance.RequestForgotPassword(_View.ForgotInputCode.text, _View.ForgotInputMobileNumber.text, _View.ForgotInputNewPassword.text, false);
            }
        }

        private void OnForgotBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ForgotDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnForgotGetCodeButton(EventData data)
        {
            if (_View.ForgotInputMobileNumber.text != "")
            {
                ClientEvent_Hall.Instance.RequestSMS(_View.ForgotInputMobileNumber.text, 3);
            }
            else
            {
                "手机号不能为空".ShowWarningTextTips();
            }
        }

        private void OnAbountWeBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.AbountWeDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnNotesItemClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UINotesItemView>();
            _View.NotesTitleText.text = view.Data.title.ToReplaceSpace();
            _View.NotesDescText.text = view.Data.content.Replace("\\n", "\n").ToReplaceSpace();
            GameManager.Instance.SetTweenAnimation(_View.NotesDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
        }

        private void OnNotesBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.NotesDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnBuyDiamondButtons(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIChargeItemView>();

            ClientEvent_Hall.Instance.RequestCreateOrder(view.Data);
        }

        private void OnMe_MyGameRoomButton(EventData data)
        {
            if (!_View.GetMyRoomDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.GetMyRoomDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            ClientEvent_Hall.Instance.RequestGetMyGameRoomLists();
            _View.MyGetRoomToggle01.isOn = true;
            MyGetRoomToggle01();
        }

        private void OnGetMyRoomDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.GetMyRoomDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnGetMyRoomTableDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.GetMyRoomTableDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnGetMyRoomItemClicked(EventData data)
        {
            var view = data.Get<GameObject>(0).Get<UIGetMyRoonItemView>();
            if (!_View.GetMyRoomTableDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.GetMyRoomTableDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            _CurrGetMyRoomListsData = view.Data;
            ClientEvent_Hall.Instance.RequestGetMyGameRoomTable(view.Data.id);
        }

        /// <summary>
        /// 俱乐部分配基金按钮
        /// </summary>
        /// <param name="data">Data.</param>
        private void OnFundListsButton(EventData data)
        {
            if (!_View.MyClubFundListsDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.MyClubFundListsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            _View.MyClubFundNumberText.text = _CurrChooseClubListsItemData.fund.ToString();
            ClientEvent_Hall.Instance.RequestFundLists(_CurrChooseClubListsItemData.id);
        }

        private void OnMyClubFundListsDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.MyClubFundListsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        /// <summary>
        /// 当前给的基金数量
        /// </summary>
        private int _CurrGiveFundNumber;

        private void OnGiveFundOkButton(EventData data)
        {
            if (_View.GiveFundNumberText.text != "" && _View.GiveFundInputField.text.ToInt() > 0)
            {
                int num = _View.GiveFundInputField.text.ToInt();
                if (num > _CurrChooseClubListsItemData.fund)
                {
                    "分配基金数额超过剩余基金金额".ShowWarningTextTips();
                    return;
                }
                _CurrGiveFundNumber = num;
                ClientEvent_Hall.Instance.RequestFundIssue(num, _CurrChooseClubListsItemData.id, _CurrChooseUIClubPeopleListItemView.MemberInfo.id);
            }
            else
            {
                "分配基金数额必须大于0".ShowWarningTextTips();
            }
        }

        /// <summary>
        /// 刷新当前俱乐部基金剩余数据
        /// </summary>
        public void RefreshFundNumber()
        {
            string.Format("成功分配 {0} 基金给该成员", _CurrGiveFundNumber).ShowWarningTextTips();
            _CurrChooseClubListsItemData.fund -= _CurrGiveFundNumber;
            _View.GiveFundNumberText.text = _CurrChooseClubListsItemData.fund.ToString();
        }

        private void OnGiveFundDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.GiveFundDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnGetMyRoomTableBaoXianMoreButton(EventData data)
        {
            if (!_View.BaoXianMoreDialog.activeInHierarchy)
            {
                GameManager.Instance.SetTweenAnimation(_View.BaoXianMoreDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            }
            if (_CurrGetMyRoomListsData.create_type == 3)
            {
                _View.BaoXianDialogRoomNameText.text = string.Format("{1} <color=grey>({0}的快速局)</color>", "联盟共享", _CurrGetMyRoomListsData.name);
            }
            else if (_CurrGetMyRoomListsData.create_type == 4)
            {
                _View.BaoXianDialogRoomNameText.text = string.Format("{1} <color=grey>({0}的快速局)</color>", "<color=yellow>官方</color>", _CurrGetMyRoomListsData.name);
            }
            else
            {
                _View.BaoXianDialogRoomNameText.text = string.Format("{1} <color=grey>({0}的快速局)</color>", _CurrGetMyRoomListsData.create_user.nick_name, _CurrGetMyRoomListsData.name);
            }
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds(_CurrGetMyRoomListsData.updated.ToString().ToInt64());
            _View.BaoXianDialogRoomTimeText.text = dt.ToLocalTime().ToString("MM/dd HH:mm");
            InitBaoXianMoreGridScroller();
        }

        private void OnBaoXianMoreDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.BaoXianMoreDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        //        private void OnCarryListsButton(EventData data)
        //        {
        //            ClientEvent_Hall.Instance.RequestCarryLists(_CurrChooseClubListsItemData.id);
        //        }

        private void OnCarryOpButton(EventData data)
        {
            //            ClientEvent_Hall.Instance.RequestCarryOp(_CurrChooseClubListsItemData.id);
            //            "功能未开放".ShowWarningTextTips();

            GameManager.Instance.SetTweenAnimation(_View.ClubManagerDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            OnClubManagerCtrlButton(null);
        }

        //        private void OnCarryResultButton(EventData data)
        //        {
        //            ClientEvent_Hall.Instance.RequestCarryResult(_CurrChooseClubListsItemData.id);
        //        }

        private void OnFX_GeRenButton(EventData data)
        {
            _CurrGetRoomOp = 1;
            _View.GridScroller_FaXian.gameObject.SetActive(true);
            _View.GridScroller_FaXianClub.gameObject.SetActive(false);
            ClientEvent_Hall.Instance.RequestGetRoomLists();
            //            _View.FX_StateImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "gerenjuann");
            _View.FX_GeRenButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
            _View.FX_ClubButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.FX_MePokerButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
        }

        private void OnFX_ClubButton(EventData data)
        {
            _CurrGetRoomOp = 1;
            _View.GridScroller_FaXian.gameObject.SetActive(false);
            _View.GridScroller_FaXianClub.gameObject.SetActive(true);
            ClientEvent_Hall.Instance.RequestGetMyClubLists();
            //            _View.FX_StateImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "juelbupaiju");
            _View.FX_GeRenButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.FX_ClubButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
            _View.FX_MePokerButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
        }

        private void OnFX_MePokerButton(EventData data)
        {
            _CurrGetRoomOp = 2;
            _View.GridScroller_FaXian.gameObject.SetActive(true);
            _View.GridScroller_FaXianClub.gameObject.SetActive(false);
            ClientEvent_Hall.Instance.RequestGetMyRoomLists();
            _View.FX_GeRenButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.FX_ClubButton.Get<Image>().color = new Color(1f, 1f, 1f, 0f);
            _View.FX_MePokerButton.Get<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        private void OnFX_ClubPokerDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.FX_ClubPokerDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnClubManagerDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.ClubManagerDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnClubManagerOpenPokerButton(EventData data)
        {
            if (_CurrClubOpType == ClubOpType.GetMyClubList)
            {
                //                if (_CurrChooseClubListsItemData.room_status != 0)
                //                {
                //                    GameManager.Instance.SetTweenAnimation(_View.FX_ClubPokerDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
                //                    ClientEvent_Hall.Instance.RequestGetClubRooms(_CurrChooseClubListsItemData.id);
                //                    return;
                //                }
                if (_CurrChooseClubListsItemData.room_status == 0)
                {
                    _View.ClubManagerDialog.SetActive(false);
                    _View.CB_MyClubListsDialog.SetActive(false);
                    _CurrClubOpType = ClubOpType.CreateClubMatch;
                    _IsCreatePokerTeam = false;
                    OnYJ_CreateMatchButtonClicked(null);
                }
                else
                {
                    "创房数量已达上限".ShowWarningTextTips();
                }
                //                if (!_CurrChooseClubListsItemData.open_create_limit || _CurrChooseClubListsItemData.is_manager || _CurrChooseClubListsItemData.create_user.id == GameManager.ClientPlayer.UserId)
                //                {
                //                    
                //                }
                //                else
                //                {
                //                    "权限不足".ShowWarningTextTips();
                //                    ClientEvent_Hall.Instance.RequestGetMyClubLists();
                //                }
            }
        }

        private void OnClubManagerTeamButton(EventData data)
        {
            _View.GridScroller_Ctrl.gameObject.SetActive(false);
            _View.ClubHistroyObj.SetActive(false);
            _View.GridScroller_Team.gameObject.SetActive(true);
            ClientEvent_Hall.Instance.RequestGetClubRooms(_CurrChooseClubListsItemData.id);
            _View.ClubManagerButtonImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "zhanduianniu");
        }

        public void OnClubManagerCtrlButton(EventData data)
        {
            _View.GridScroller_Ctrl.gameObject.SetActive(true);
            _View.ClubHistroyObj.SetActive(false);
            _View.GridScroller_Team.gameObject.SetActive(false);
            _View.ClubManagerButtonImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "kongzhianniu");
            ClientEvent_Hall.Instance.RequestCarryLists(_CurrChooseClubListsItemData.id);
        }

        private void OnClubManagerListsButton(EventData data)
        {
            _View.GridScroller_Ctrl.gameObject.SetActive(false);
            _View.ClubHistroyObj.SetActive(true);
            _View.GridScroller_Team.gameObject.SetActive(false);
            _View.ClubManagerButtonImage.sprite = AtlasMapping.Instance.GetAtlas("Tadd", "shengheanniu");
            ClientEvent_Hall.Instance.RequestCarryResult(_CurrChooseClubListsItemData.id);
        }

        private void OnClubCtrlItemOkClicked(EventData data)
        {
            var ctrlData = data.Get<GameObject>(0).Get<UIClubCtrlltemView>().Data;
            ClientEvent_Hall.Instance.RequestCarryOp(ctrlData.CtrlApplyMemberInfo.apply_carry, true, ctrlData.CtrlApplyMemberInfo.club_id, ctrlData.CtrlApplyMemberInfo.player_id, ctrlData.CtrlApplyMemberInfo.room_id);
        }

        private void OnClubCtrlItemCancelClicked(EventData data)
        {
            var ctrlData = data.Get<GameObject>(0).Get<UIClubCtrlltemView>().Data;
            ClientEvent_Hall.Instance.RequestCarryOp(ctrlData.CtrlApplyMemberInfo.apply_carry, false, ctrlData.CtrlApplyMemberInfo.club_id, ctrlData.CtrlApplyMemberInfo.player_id, ctrlData.CtrlApplyMemberInfo.room_id);
        }

        private void OnNotOpenButton(EventData data)
        {
            "功能尚未开放".ShowWarningTextTips();
        }

        private void OnGiveFundListsDialogBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.GiveFundListsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnGiveFundListsButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.GiveFundListsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            _View.GiveFundDialogNumberText.text = _CurrChooseClubListsItemData.fund.ToString();
            ClientEvent_Hall.Instance.RequestGetClubDetail(_CurrChooseClubListsItemData.id);
        }

        //        private void OnMePokerButton(EventData data)
        //        {
        //            _View.MePokerButton.SetActive(false);
        //            _View.AllPokerButton.SetActive(true);
        //            ClientEvent_Hall.Instance.RequestGetClubRooms(_CurrChooseClubListsItemData.id);
        //        }
        //
        //        private void OnAllPokerButton(EventData data)
        //        {
        //            _View.MePokerButton.SetActive(true);
        //            _View.AllPokerButton.SetActive(false);
        //            ClientEvent_Hall.Instance.RequestGetClubRooms(_CurrChooseClubListsItemData.id);
        //        }

        private void OnWalletBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.WalletDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnBTCButton(EventData data)
        {
            _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "BTC");
            _CurrChooseWallType = EWalletType.BTC;
            SetWalletPublicData();
        }

        private void OnBCHButton(EventData data)
        {
            _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "BCH");
            _CurrChooseWallType = EWalletType.BCH;
            SetWalletPublicData();
        }

        private void OnLTCButton(EventData data)
        {
            _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "LTC");
            _CurrChooseWallType = EWalletType.LTC;
            SetWalletPublicData();
        }

        private void OnETHButton(EventData data)
        {
            _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "ETH");
            _CurrChooseWallType = EWalletType.ETH;
            SetWalletPublicData();
        }

        private void OnDASHButton(EventData data)
        {
            _View.WalletDialogView.WalletImage.sprite = AtlasMapping.Instance.GetAtlas("Wallet", "DASH");
            _CurrChooseWallType = EWalletType.DASH;
            SetWalletPublicData();
        }

        private void OnMaiRuButton(EventData data)
        {
            _View.WalletDialogView.GoumaiObj.SetActive(true);
            SetGMDialogData();
        }

        private void OnMaiChuButton(EventData data)
        {
            _View.WalletDialogView.MaiChuObj.SetActive(true);
            SetMCDialogData();
        }

        private void OnChongBiButton(EventData data)
        {
            _View.WalletDialogView.CBObj.SetActive(true);
            SetCBDailogData();
        }

        private void OnTiBiButton(EventData data)
        {
            //            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            //            {
            //                var walletdata = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
            ////                if (walletdata.has_qr)
            ////                {
            ////                    _View.WalletDialogView.TBObj.SetActive(true);
            ////                    SetTBDialogData();
            ////                }
            ////                else
            ////                {
            ////                    "暂不满足提币资格".ShowWarningTextTips();
            ////                }
            //
            //            }
            _View.WalletDialogView.TBObj.SetActive(true);
            SetTBDialogData();
        }

        private void OnCBObjCloseButton(EventData data)
        {
            _View.WalletDialogView.CBObj.SetActive(false);
        }

        private void OnCBCopyButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletdata = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                GameManager.Instance.CopyToClipboard(walletdata.address);
                "复制钱包地址成功".ShowWarningTextTips();
            }
        }

        private void OnTBObjCloseButton(EventData data)
        {
            _View.WalletDialogView.TBObj.SetActive(false);
        }

        private void OnTBAllButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletdata = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.TBNumberInputField.text = (walletdata.amount.ToFloat() - walletdata.fee.ToFloat()).ToString();
            }
        }

        private void OnTBOkButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletData = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                if (walletData.has_qr)
                {
                    if (_View.WalletDialogView.TBReciveAdressInputField.text != "")
                    {
                        if (_View.WalletDialogView.TBNumberInputField.text != "")
                        {
                            if (_View.WalletDialogView.TBNumberInputField.text.ToFloat() != 0)
                            {
                                if (_View.WalletDialogView.TBNumberInputField.text.ToFloat() <= (walletData.amount.ToFloat() - walletData.fee.ToFloat()))
                                {
                                    ClientEvent_Hall.Instance.RequestWithDraw(walletData.address, _View.WalletDialogView.TBNumberInputField.text, _CurrChooseWallType.ToString(), _View.WalletDialogView.TBReciveAdressInputField.text);
                                }
                                else
                                {
                                    "提币数量超出可用余额".ShowWarningTextTips();
                                }
                            }
                            else
                            {
                                "提币数量必须大于0".ShowWarningTextTips();
                            }
                        }
                        else
                        {
                            "提币数量不能为空".ShowWarningTextTips();
                        }
                    }
                    else
                    {
                        "接收地址不能为空".ShowWarningTextTips();
                    }
                }
                else
                {
                    "暂不满足提币资格".ShowWarningTextTips();
                }
            }
        }

        private void OnGoumaiObjCloseButton(EventData data)
        {
            _View.WalletDialogView.GoumaiObj.SetActive(false);
        }

        private void OnMaiChuObjCloseButton(EventData data)
        {
            _View.WalletDialogView.MaiChuObj.SetActive(false);
        }

        private void OnWalletLogsButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.WalletDialogView.WalletLogsDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            ClientEvent_Hall.Instance.RequestWalletLogs(0);
        }

        private void OnGMAllButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletData = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                _View.WalletDialogView.GMNumberInputField.text = walletData.amount;
            }
        }

        private void OnMCAllButton(EventData data)
        {
            _View.WalletDialogView.MCNumberInputField.text = GameManager.ClientPlayer.Balance.ToString();
        }

        private void OnGMOkButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletData = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                if (_View.WalletDialogView.GMNumberInputField.text != "")
                {
                    if (_View.WalletDialogView.GMNumberInputField.text.ToFloat() <= walletData.amount.ToFloat())
                    {
                        ClientEvent_Hall.Instance.RequestWalletBuy(walletData.address, _View.WalletDialogView.GMNumberInputField.text, 1, walletData.code, _CurrChooseWallType.ToString());
                    }
                    else
                    {
                        "金额不足".ShowWarningTextTips();
                    }
                }
                else
                {
                    "数量不能为空".ShowWarningTextTips();
                }
            }
        }

        private void OnMCOkButton(EventData data)
        {
            if (_WalletItemInfoDics.ContainsKey(_CurrChooseWallType.ToString()))
            {
                var walletData = _WalletItemInfoDics[_CurrChooseWallType.ToString()];
                if (_View.WalletDialogView.MCNumberInputField.text != "")
                {
                    if (_View.WalletDialogView.MCNumberInputField.text.ToInt() <= GameManager.ClientPlayer.Balance)
                    {
                        ClientEvent_Hall.Instance.RequestSell(walletData.address, "1", _View.WalletDialogView.MCNumberInputField.text.ToInt(), walletData.code, _CurrChooseWallType.ToString());
                    }
                    else
                    {
                        "金额不足".ShowWarningTextTips();
                    }
                }
                else
                {
                    "数量不能为空".ShowWarningTextTips();
                }
            }
        }

        private void OnRefreshButton(EventData data)
        {
            ClientEvent_Hall.Instance.RequestWalletLogs(2);
        }

        private void OnWalletLogsBackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.WalletDialogView.WalletLogsDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
        }

        private void OnWalletLogsCBButton(EventData data)
        {
            SetWalletLogsData(1);
        }

        private void OnWalletLogsTBButton(EventData data)
        {
            SetWalletLogsData(2);
        }

        private void OnWalletLogsMRButton(EventData data)
        {
            SetWalletLogsData(3);
        }

        private void OnWalletLogsMCButton(EventData data)
        {
            SetWalletLogsData(4);
        }

        private void OnCameraButton(EventData data)
        {
            _IconSize = _View.Me_HeadImage.rectTransform.rect.size;
            #if UNITY_IOS
            PickImage.Init(OnPickImageCallback, null);
            PickImage.OpenPhotoLib(PickImage.EOpenPhotoSource.Camera, _IconSize.x, _IconSize.y);
            #endif
            _View.ChooseTypeDialog.SetActive(false);
        }

        private void OnPhotoButton(EventData data)
        {
            if (_CurrUploadType == 1)
            {
                _IconSize = _View.Me_HeadImage.rectTransform.rect.size;
            }
            else
            {
                _IconSize = _View.MyClubHeadImageButton.rectTransform.rect.size;
            }
            #if UNITY_IOS
            PickImage.Init(OnPickImageCallback, null);
            PickImage.OpenPhotoLib(PickImage.EOpenPhotoSource.PhotoLibrary, _IconSize.x, _IconSize.y);
            #endif
            _View.ChooseTypeDialog.SetActive(false);
        }

        private void OnCloseChooseTypeDialogButton(EventData data)
        {
            _View.ChooseTypeDialog.SetActive(false);
        }

        private void OnCloseChooseTypeDialogButton02(EventData data)
        {
            _View.ChooseTypeDialog.SetActive(false);
        }

        /// <summary>
        /// 当前操作上传头像的类型 1-个人  2-俱乐部
        /// </summary>
        private int _CurrUploadType;
        private string _CurrChooseHeadData;
        private int _CurrChooseGender;

        private void OnHeadImageButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.EditorUserMessageDialog, new Vector3(764, 0, 0), new Vector3(0, 0, 0), true);
            _CurrChooseHeadData = "";
            _CurrChooseGender = GameManager.ClientPlayer.Gender;
            _View.Editor_ChangeHeadImageButton.sprite = GameManager.ClientPlayer.HeadSprite;
            _View.InputField_PlayerName.text = GameManager.ClientPlayer.Name;
            _View.AreaText.text = GameManager.ClientPlayer.AreaCode;
            _View.InputField_Remarks.text = GameManager.ClientPlayer.Reamrks;
            if (_CurrChooseGender == 2)
            {
                _View.SexObj.transform.localPosition = new Vector3(25, 0, 0);
            }
            else
            {
                _View.SexObj.transform.localPosition = new Vector3(-25, 0, 0);
            }
        }

        private void OnMyClubHeadImageButton(EventData data)
        {
            if (_CurrChooseClubListsItemData.is_manager)
            {
                _CurrChooseHeadData = "";
                #if UNITY_IOS
                _View.ChooseTypeDialog.SetActive(true);
                #elif UNITY_ANDROID
                OpenPhoto();
                #endif
                _CurrUploadType = 2;
            }
            else
            {
                "创始人和管理员才有权限修改俱乐部头像".ShowWarningTextTips();
            }
        }

        void OnPickImageCallback(byte[] imageData)
        {
            var tex = new Texture2D((int)_IconSize.x, (int)_IconSize.y, TextureFormat.ARGB32, false);
            tex.LoadImage(imageData);
        
            _CurrChooseHeadData = Convert.ToBase64String(imageData);

            //字节数组转成base64字节发送给服务器
            if (_CurrUploadType == 1)
            {
                _View.Editor_ChangeHeadImageButton.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
//                ClientEvent_Hall.Instance.RequestClubUploadAvatar(_CurrChooseHeadData, _CurrChooseClubListsItemData.id);

                _View.MyClubHeadImageButton.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }

        public void OnEditor_BackButton(EventData data)
        {
            GameManager.Instance.SetTweenAnimation(_View.EditorUserMessageDialog, new Vector3(0, 0, 0), new Vector3(764, 0, 0), false);
            if (data == null)
            {
                GameManager.ClientPlayer.HeadSprite = _View.Editor_ChangeHeadImageButton.sprite;

                ClientEvent_Hall.Instance.RequestGetBaseDetail();
                ClientEvent_Hall.Instance.RequestUserInfo();
                ClientEvent_Hall.Instance.RequestGetMessage();

                StartCoroutine(UpdateUserInfo());
            }
        }

        IEnumerator UpdateUserInfo()
        {
            yield return new WaitForSeconds(0.15f);
            _View.Me_HeadImage.sprite = GameManager.ClientPlayer.HeadSprite;
            _View.Me_GenderIconImage.sprite = AtlasMapping.Instance.GetAtlas("Other", PokerUtility.GetGenderIconSpriteName(GameManager.ClientPlayer.Gender));
            _View.Me_GenderIconImage.SetNativeSize();
        }

        private void OnEditor_SaveButton(EventData data)
        {
            if (_View.InputField_PlayerName.text == "")
            {
                "用户名不能为空".ShowWarningTextTips();
                return;
            }

            if (_CurrChooseHeadData != "")
            {
                ClientEvent_Hall.Instance.RequestUploadAvatar(_CurrChooseHeadData);
            }

            ClientEvent_Hall.Instance.RequestUpdateUserInfo(_CurrChooseGender, _View.InputField_PlayerName.text, _View.InputField_Remarks.text, _View.AreaText.text);
        }

        private void OnEditor_ChangeHeadImageButton(EventData data)
        {
            #if UNITY_IOS
            _View.ChooseTypeDialog.SetActive(true);
            #elif UNITY_ANDROID
            OpenPhoto();
            #endif
            _CurrUploadType = 1;
        }

        private void OnChooseGenderButton(EventData data)
        {
            if (_CurrChooseGender == 1)
            {
                _CurrChooseGender = 2;
                _View.SexObj.transform.localPosition = new Vector3(25, 0, 0);
            }
            else
            {
                _CurrChooseGender = 1;
                _View.SexObj.transform.localPosition = new Vector3(-25, 0, 0);
            }
        }

        private void OnEditor_ChooseAreaButton(EventData data)
        {
            GameManager.UI.OpenUIForm(UIFormId.UIChooseAreaPanel);
        }

        #endregion

        #region android pickimage

        /// <summary>
        ///  获取用户头像状态信息
        /// </summary>
        public enum ENUM_AVATAR_RESULT
        {
            eResult_Failed,
            // 失败
            eResult_Camera,
            // 打开照相机
            eResult_Picture,
            // 打开相册
            eResult_Cancel,
            // 取消
            eResult_Success,
            // 成功
            eResult_Finish
            // 关闭原生界面
        }

        private void OpenPhoto()
        {
            AndroidJavaClass androidActivityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject androidJavaObject = androidActivityClass.GetStatic<AndroidJavaObject>("currentActivity");
            if (androidJavaObject == null)
            {
                return;
            }
            androidJavaObject.Call("SettingAvaterFormMobile", this.gameObject.name, "OnAvaterCallBack", "image.png");
        }

        void OnAvaterCallBack(string strResult)
        {
            if (strResult.Equals(ENUM_AVATAR_RESULT.eResult_Success.ToString()))
            {
                // 成功
                StartCoroutine(LoadTexture("image.png"));
            }
            else if (strResult.Equals(ENUM_AVATAR_RESULT.eResult_Cancel.ToString()))
            {
            }
            else if (strResult.Equals(ENUM_AVATAR_RESULT.eResult_Failed.ToString()))
            {
            }
        }

        IEnumerator LoadTexture(string name)
        {
            string path = "file://" + Application.persistentDataPath + "/" + name;
            WWW www = new WWW(path);
            yield return www;

            //字节数组转成base64字节发送给服务器
            _CurrChooseHeadData = Convert.ToBase64String(www.texture.EncodeToPNG());
            if (_CurrUploadType == 1)
            {
                _View.Editor_ChangeHeadImageButton.sprite = Sprite.Create(www.texture, new Rect(0f, 0f, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
//                ClientEvent_Hall.Instance.RequestClubUploadAvatar(_CurrChooseHeadData, _CurrChooseClubListsItemData.id);

                _View.MyClubHeadImageButton.sprite = Sprite.Create(www.texture, new Rect(0f, 0f, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        #endregion
    }

}