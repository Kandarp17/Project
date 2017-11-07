package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;


/**
 * A simple {@link Fragment} subclass.
 */
public class SiteDocumentFragment extends Fragment implements AdapterView.OnItemClickListener {

    ListView lv;
    ArrayList<String> docname=new ArrayList();
    ArrayList<String> doclink=new ArrayList();
    ArrayList<HashMap<String, String>> data = new ArrayList<>();
    String[] from = {"docname", "status" };
    int[] to={R.id.text1, R.id.text2};
    String link,unm,pass;
    View v1;
    public SiteDocumentFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1=inflater.inflate(R.layout.fragment_site_document, container, false);
        v1=inflater.inflate(R.layout.fragment_staff_list, container, false);
        SharedPreferences sp1=v1.getContext().getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        lv=(ListView)v1.findViewById(R.id.list);
        lv.setOnItemClickListener(this);
        WebTask wb=new WebTask();
        wb.execute(unm,pass);

        return v1;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

        String pdfd = link+doclink.get(position); //YOUR URL TO PDF
        String url = "http://docs.google.com/gview?embedded=true&url="+pdfd;
        Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(pdfd));
        startActivity(browserIntent);
    }

    /*@Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        Intent i=new Intent(this,Main3Activity.class);
        i.putExtra("placeid",arrid.get(position));
        startActivity(i);

    }*/
    private class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(v1.getContext());
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/api/SiteDocumentAPI/GetSiteDocument");
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {
                JSONArray arr=new JSONArray(ans);
                for(int i=0;i<arr.length();i++) {
                    JSONObject obj = arr.getJSONObject(i);
                    docname.add(obj.getString("DocumentName"));
                    String path=obj.getString("DocumentPath");
                    HashMap<String, String> map = new HashMap<>();
                    map.put("docname",obj.getString("DocumentName"));
                    map.put("status",obj.getString("Status"));
                    data.add(map);
                    doclink.add(path);

                }


            } catch (JSONException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }

            return "";
        }
        @Override
        protected void onPostExecute(String ans){
            super.onPostExecute(ans);
            try{
                Dialog.dismiss();

            }
            catch (Exception e){

            }
            SimpleAdapter sa= new SimpleAdapter(v1.getContext(), data,R.layout.list_of_2, from, to);
            lv.setAdapter(sa);


        }
    }

}
