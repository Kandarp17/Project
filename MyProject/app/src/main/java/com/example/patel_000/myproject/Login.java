package com.example.patel_000.myproject;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Typeface;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class Login extends AppCompatActivity {
    EditText pswd,usrusr;
    String password,username,link;
    TextView fpass,lin;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
            link= getResources().getString(R.string.url);
            SharedPreferences sp1=this.getSharedPreferences("Login",0);
            String unm=sp1.getString("Unm", null);
            String pass = sp1.getString("Psw", null);
        if(unm!=null && pass!=null){
            Intent it = new Intent(Login.this, MainActivity.class);
            it.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
            startActivity(it);
        }
        lin = (TextView) findViewById(R.id.lin);
        usrusr = (EditText) findViewById(R.id.usrusr);
        pswd = (EditText) findViewById(R.id.pswrdd);
        fpass = (TextView) findViewById(R.id.fpass);

        lin.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                password=pswd.getText().toString();
                username=usrusr.getText().toString();
                WebTask wb=new WebTask();
                wb.execute(username,password);

            }
        });
        fpass.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent it = new Intent(Login.this, Forgetpass.class);
                startActivity(it);

            }
        });
    }
    private class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(Login.this);
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/Access/SignIn");
            request.setMethod("GET");


            request.setParam("Username",username);
            request.setParam("Password",password);

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

                SharedPreferences sp=getSharedPreferences("Login", 0);
                SharedPreferences.Editor Ed=sp.edit();
                Ed.putString("Unm",username );
                Ed.putString("Psw",password);
                Ed.commit();
                Intent i = new Intent(Login.this,MainActivity.class);
                i.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(i);
            }
            else{
                Toast.makeText(Login.this,ans +"Invalid username or password", Toast.LENGTH_LONG).show();
            }

            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
