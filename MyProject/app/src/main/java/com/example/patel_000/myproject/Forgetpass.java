package com.example.patel_000.myproject;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Created by patel_000 on 10-03-2017.
 */

public class Forgetpass extends AppCompatActivity {
    EditText pswd,usrusr;
    String username,link;
    LinearLayout l;
    TextView fpass,lin;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        lin = (TextView) findViewById(R.id.lin);
        link= getResources().getString(R.string.url);
        usrusr = (EditText) findViewById(R.id.usrusr);
        l = (LinearLayout) findViewById(R.id.layoutpass);
        l.setVisibility(View.GONE);
        fpass = (TextView) findViewById(R.id.fpass);
        fpass.setText("back");
        lin.setText("submit");

        lin.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                username=usrusr.getText().toString();
                WebTask wb=new WebTask();
                wb.execute(username);

            }
        });
        fpass.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent it = new Intent(Forgetpass.this, Login.class);
                startActivity(it);

            }
        });
    }
    private class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(Forgetpass.this);
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/Access/ForgetMobile");
            request.setMethod("GET");


            request.setParam("Username",username);


            String ans = (String) HttpManager.getData(request);



            return ans;
        }
        @Override
        protected void onPostExecute(String ans){
            super.onPostExecute(ans);
            try{
                Dialog.dismiss();

            }
            catch (Exception e){

            }

            if(ans.startsWith("Success")){
                Toast.makeText(Forgetpass.this,"Check Email", Toast.LENGTH_LONG).show();
                Intent i = new Intent(Forgetpass.this,Login.class);
                i.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(i);

            }
            else{
                Toast.makeText(Forgetpass.this,"Invalid username or somthing went wrong", Toast.LENGTH_LONG).show();
            }

            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
