package com.example.patel_000.myproject;

import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class IssueDetails extends AppCompatActivity {

    LinearLayout reply;
    TextView title,detail,status,reportdate,replydate,replymessage;
    String Title,Detail,Status,Reportdate,Replydate,Replymessage,id;
    View v1;
    String unm,pass,link;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_issue_details);
        //Bundle bundle = this.getArgument();
        //id= bundle.getString("id","Test");
        id=getIntent().getStringExtra("id");
        findAllView();
        SharedPreferences sp1=getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        WebTask wb= new WebTask();
        wb.execute();
    }
    private void findAllView() {
        title=(TextView)findViewById(R.id.titlevalue);
        detail=(TextView)findViewById(R.id.detailvalue);
        status=(TextView)findViewById(R.id.statusvalue);
        reportdate=(TextView)findViewById(R.id.reportdatevalue);
        replydate=(TextView)findViewById(R.id.replydatevalue);
        replymessage=(TextView)findViewById(R.id.replymessagevalue);
        reply=(LinearLayout)findViewById(R.id.reply);

    }
    public class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(IssueDetails.this);
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/api/IssueAPI/AllIssue");
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {

                JSONArray arr=new JSONArray(ans);
                for(int i=0;i<arr.length();i++) {
                    JSONObject obj = arr.getJSONObject(i);

                    if (id.equals(obj.getString("IssueID"))) {
                        Title = obj.getString("Title");
                        Detail = obj.getString("Details");
                        Status = obj.getString("Status");
                        Reportdate = obj.getString("ReportingTime");
                        Replydate = obj.getString("ReplyTime");
                        Replymessage = obj.getString("ReplyMsg");
                    }
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
            if(Status.equals("Pending")){
                reply.setVisibility(View.GONE);

            }

            title.setText(Title);
            detail.setText(Detail);
            status.setText(Status);
            reportdate.setText(Reportdate);
            replydate.setText(Replydate);
            replymessage.setText(Replymessage);



            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
