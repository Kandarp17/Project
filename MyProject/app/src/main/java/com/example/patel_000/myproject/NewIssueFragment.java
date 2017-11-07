package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.media.session.MediaSessionCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;


/**
 * A simple {@link Fragment} subclass.
 */
public class NewIssueFragment extends Fragment implements View.OnClickListener {
    Button submit;
    EditText title, details;
    View v1;
    String link,unm,pass,t,d;

    public NewIssueFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        // Inflate the layout for this fragment
        v1 = inflater.inflate(R.layout.fragment_new_issue, container, false);
        findAllViews();
        link = getResources().getString(R.string.url);
        SharedPreferences sp1 = v1.getContext().getSharedPreferences("Login", 0);
         unm = sp1.getString("Unm", null);
         pass = sp1.getString("Psw", null);
        submit.setOnClickListener(this);
        return v1;
    }

    public void findAllViews() {
        submit = (Button) v1.findViewById(R.id.btnsumbit);
        title = (EditText) v1.findViewById(R.id.laytitle);
        details = (EditText) v1.findViewById(R.id.laydetails);
    }

    @Override
    public void onClick(View v) {

        if (v == submit) {
            t=title.getText().toString();
            d=details.getText().toString();
            WebTask wb=new WebTask();
            wb.execute(unm,pass);
        }
    }

    private class WebTask extends AsyncTask<String, String, String> {

        ProgressDialog Dialog = null;


        @Override
        protected void onPreExecute() {
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
            RequestPackage request = new RequestPackage();
            request.setUri(link + "/api/IssueAPI/GetMessage");
            request.setMethod("GET");

            request.setParam("Username",unm);
            request.setParam("Password",pass);
            request.setParam("Title",t);
            request.setParam("Details",d);
            String ans = (String) HttpManager.getData(request);


            return ans;
        }

        @Override
        protected void onPostExecute(String ans) {
            super.onPostExecute(ans);
            try {
                Dialog.dismiss();

            } catch (Exception e) {

            }

            if (ans.startsWith("\"Issue Reported\"")) {
                Toast.makeText(v1.getContext(), ans , Toast.LENGTH_LONG).show();
            }
            else {
                Toast.makeText(v1.getContext(), ans + "Try again", Toast.LENGTH_LONG).show();
            }

            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
