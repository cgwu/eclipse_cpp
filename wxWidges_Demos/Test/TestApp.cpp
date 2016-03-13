/***************************************************************
 * Name:      TestApp.cpp
 * Purpose:   Code for Application Class
 * Author:     ()
 * Created:   2016-03-13
 * Copyright:  ()
 * License:
 **************************************************************/

#include "TestApp.h"

//(*AppHeaders
#include "TestMain.h"
#include <wx/image.h>
//*)

IMPLEMENT_APP(TestApp);

bool TestApp::OnInit()
{
    //(*AppInitialize
    bool wxsOK = true;
    wxInitAllImageHandlers();
    if ( wxsOK )
    {
    	TestDialog Dlg(0);
    	SetTopWindow(&Dlg);
    	Dlg.ShowModal();
    	wxsOK = false;
    }
    //*)
    return wxsOK;

}
