using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using GameDefine_Talk;
using UnityEngine.UI;

public class UserInfoData
{
    public string someText;

    /// <summary>
    /// We will store the cell size in the model so that the cell view can update it
    /// </summary>
    public float cellSize;

    // 아바타 이미지 
    public bool bAvatarImage;

    //  유저 거리 
    public string strDistance;

    // 유저 이름
    public string strUserName;

    // 성별 
    public string strSex;

    // 나이 
    public string strAge;

    // 1: 쪽지방(클릭해서 대화방이동가능), 0: 그외방 
    public bool bChatRoomActive;

    // 돈
    public int nGold;

    // 시간 
    public string strTime;  // 2019-07-22


    // 메시지 컨트롤 아이콘 이미지
    public SmallList<MessageControlData> m_MessageControlData = new SmallList<MessageControlData>();
}

public class MessageControlData
{
    public bool m_bActive;
    public string m_strFile;
}

public class UserInfoList : MonoBehaviour, IEnhancedScrollerDelegate
{

    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private SmallList<UserInfoData> _data;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. Note that you can use more
    /// than one kind of cell, but this example just has the one type.
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    void Awake()
    {
        _data = new SmallList<UserInfoData>();
    }

    /// <summary>
    /// Be sure to set up your references to the scroller after the Awake function. The 
    /// scroller does some internal configuration in its own Awake function. If you need to
    /// do this in the Awake function, you can set up the script order through the Unity editor.
    /// In this case, be sure to set the EnhancedScroller's script before your delegate.
    /// 
    /// In this example, we are calling our initializations in the delegate's Start function,
    /// but it could have been done later, perhaps in the Update function.
    /// </summary>
    void Start()
    {
        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;
    }

    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private void LoadLargeData()
    {
        // set up some simple data
        _data = new SmallList<UserInfoData>();
        for (var i = 0; i < 1000; i++)
            _data.Add(new UserInfoData() { someText = "Cell Data Index " + i.ToString() });

        // tell the scroller to reload now that we have the data
        //scroller.ReloadData();
        ResizeScroller();
    }

    public void AddUserInfoDatas(ref SmallList<UserInfoData> userInfoDatas)
    {  
        _data = userInfoDatas;
    }

    public void AddUserInfoData( int ID, string strNick, bool bAvatarImage = false, SmallList<MessageControlData> MCDatas = null )
    {
        UserInfoData data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = bAvatarImage;
        data.m_MessageControlData = MCDatas;
        data.someText = (ID * 11 + 0).ToString() + strNick;
        _data.Add(data);
    }

    /// <summary>
    /// Populates the data with a small set of records
    /// </summary>
    private void LoadSmallData()
    {
        // set up some simple data
        _data = new SmallList<UserInfoData>();

        _data.Add(new UserInfoData() { someText = "A" });
        _data.Add(new UserInfoData() { someText = "B" });
        _data.Add(new UserInfoData() { someText = "C" });

        // tell the scroller to reload now that we have the data
        //scroller.ReloadData();
        ResizeScroller();
    }

    #region UI Handlers

    /// <summary>
    /// Button handler for the large data loader
    /// </summary>
    public void LoadLargeDataButton_OnClick()
    {
        LoadLargeData();
    }

    /// <summary>
    /// Button handler for the small data loader
    /// </summary>
    public void LoadSmallDataButton_OnClick()
    {
        LoadSmallData();
    }

    public void ResizeScroller()
    {
        // capture the scroller dimensions so that we can reset them when we are done
        var rectTransform = scroller.GetComponent<RectTransform>();
        var size = rectTransform.sizeDelta;

        // set the dimensions to the largest size possible to acommodate all the cells
        rectTransform.sizeDelta = new Vector2(size.x, float.MaxValue);

        // First Pass: reload the scroller so that it can populate the text UI elements in the cell view.
        // The content size fitter will determine how big the cells need to be on subsequent passes
        scroller.ReloadData();

        // reset the scroller size back to what it was originally
        rectTransform.sizeDelta = size;

        // Second Pass: reload the data once more with the newly set cell view sizes and scroller content size
        scroller.ReloadData();
    }

    #endregion

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // in this example, we just pass the number of our data elements
        return _data.Count;
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return _data[dataIndex].cellSize;
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        UserInfoCellView cellView = scroller.GetCellView(cellViewPrefab) as UserInfoCellView;

        // in this example, we just pass the data to our cell's view which will update its UI
        cellView.SetData(dataIndex, _data[dataIndex]);

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}   
